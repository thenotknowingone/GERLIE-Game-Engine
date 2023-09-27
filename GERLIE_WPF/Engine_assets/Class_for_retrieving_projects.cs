using System;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Linq;
using GERLIE_WPF.Utilities;

namespace GERLIE_WPF.Engine_assets
{
    [DataContract]
    public class Class_for_project_data                                                                                                                                         //Class for keeping project data.
    {
        [DataMember]
        public string Project_name
        {
            get;
            set;
        }
        [DataMember]
        public string Project_path
        {
            get;
            set;
        }
        [DataMember]
        public DateTime Project_date
        {
            get;
            set;
        }
        public string Full_path
        {
            get => $"{Project_path}{Project_name}{Class_for_central_data_structure.Extension}";
        }
        public byte[] Icon
        {
            get;
            set;
        }
        public byte[] Screenshot
        {
            get;
            set;
        }
    }
    [DataContract]
    public class Class_for_project_data_list
    {
        [DataMember]
        public List<Class_for_project_data> Project_data_list        
        {
            get;
            set;
        }
    }
    class Class_for_retrieving_projects                                                                                                                                         //The class needed to save data based on Class_for_project_data dataset.
    {
        private static readonly string _application_data_path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\GERLIEGameEngine\";                   //Constant data that contains the path to save the project data list.
        private static readonly string _project_data_path;
        private static readonly ObservableCollection<Class_for_project_data> _projects = new ObservableCollection<Class_for_project_data>();
        public static ReadOnlyObservableCollection<Class_for_project_data> Projects
        {
            get;
        }
        private static void Read_project_data()
        {
                if (File.Exists(_project_data_path))
                {
                    var projects = Class_for_serialization.From_file_method<Class_for_project_data_list>(_project_data_path).Project_data_list.OrderByDescending(x => x.Project_date);

                    _projects.Clear();

                    foreach (var project in projects)
                    {
                        if (File.Exists(project.Full_path))
                        {
                            project.Icon = File.ReadAllBytes($@"{project.Project_path}\.gerlie\Icon.ICO");
                            project.Screenshot = File.ReadAllBytes($@"{project.Project_path}\.gerlie\Screenshot.png");
                            _projects.Add(project);
                        }
                    }
                }
        }
        private static void Write_project_data()
        {
            var projects = _projects.OrderBy(x => x.Project_date).ToList();                                                                                                     //Sorts the elements in the _projects collection by their Project_date property in ascending order and stores the result as a list in the projects variable.
            Class_for_serialization.To_file_method(new Class_for_project_data_list() { Project_data_list = projects }, _project_data_path);                                     //Invokes a custom method To_file_method in the Class_for_serialization class, passing in an instance of Class_for_project_data_list with data to be serialized and specifying the file path for saving the serialized data.
        }
        public static Class_for_central_data_structure Open(Class_for_project_data project_data)
        {
            Read_project_data();
            
            var project = _projects.FirstOrDefault(x => x.Full_path == project_data.Full_path);

            if(project != null)
            {
                project.Project_date = DateTime.Now;
            }
            else
            {
                project = project_data;
                project.Project_date = DateTime.Now;
                _projects.Add(project);
            }
            Write_project_data();

            return Class_for_central_data_structure.Load(project.Full_path);
        }
        static Class_for_retrieving_projects()
        {
            try
            {
                if(!Directory.Exists(_project_data_path)) 
                        Directory.CreateDirectory(_application_data_path);
                
                _project_data_path = $@"{_application_data_path}project_data.xml";
                Projects = new ReadOnlyObservableCollection<Class_for_project_data>(_projects);

                Read_project_data();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        
    }
}
