using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Linq;
using GERLIE_Editor.Utilities;

namespace GERLIE_Editor.Engine_assets
{
    [DataContract]
    public class Project_data
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
        public DateTime Date
        {
            get;
            set;
        }
        public string Full_path 
        { 
            get => $"{Project_path}{Project_name}{Project_class.Extension}"; 
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
    public class Project_data_list
    {
        [DataMember]
        public List<Project_data> Projects
        { 
            get; 
            set; 
        }
    }
    class Open_project_class
    {
        private static readonly string _application_data_path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\GERLIEGameEngine\";
        private static readonly string _project_data_path;
        private static readonly ObservableCollection<Project_data> _projects = new ObservableCollection<Project_data>();
        public static ReadOnlyObservableCollection<Project_data> Projects
        {
            get;
        }
        private static void Read_project_data()
        {
            if(File.Exists(_project_data_path)) 
            {
                var projects = Serializer.From_file<Project_data_list>(_project_data_path).Projects.OrderByDescending(x => x.Date);
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
            var projects = _projects.OrderBy(x => x.Date).ToList();
            Serializer.To_file(new Project_data_list() { Projects = projects }, _project_data_path);
        }
        public static Project_class Open(Project_data data)
        {
            Read_project_data();
            var project = _projects.FirstOrDefault(x => x.Full_path == data.Full_path);
            if (project != null)
            {
                project.Date = DateTime.Now;
            }
            else
            {
                project = data;
                project.Date = DateTime.Now;
                _projects.Add(project);
            }
            Write_project_data();

            return Project_class.Load(project.Full_path);
        }
        static Open_project_class() 
        {
            try
            {
                if (!Directory.Exists(_application_data_path)) Directory.CreateDirectory(_application_data_path);
                _project_data_path = $@"{_application_data_path}project_data.xml";
                Projects = new ReadOnlyObservableCollection<Project_data>(_projects);
                Read_project_data();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
