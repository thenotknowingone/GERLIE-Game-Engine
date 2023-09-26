using GERLIE_WPF.Common;
using GERLIE_WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace GERLIE_WPF.Engine_assets
{
    [DataContract]
    public class Class_for_project_templates                                                                                                    //The Class_for_project_templates defines a template for storing project-related information, including project type, associated file, and a list of folders, using properties to access and modify these values.
    {
        [DataMember]
        public string Project_type
        {
            get;
            set;
        }
        [DataMember]
        public string Project_file
        {
            get;
            set;
        }
        [DataMember]
        public List<string>Folders
        {
            get;
            set;
        }
        public byte[] Icon
        {
            get; set;
        }
        public byte[] Screenshot
        {
            get; set;
        }
        public string Icon_file_path
        {
            get; set;
        }
        public string Screenshot_file_path
        {
            get; set;
        }
        public string Project_file_path
        {
            get; set;
        }
    }
    class Class_for_new_projects : View_model_base                                                                                              //Class for new projects inheriting from INPC VM.
    {
        private readonly string _template_path = @"E:\Programming Projects\GERLIE\GERLIE_WPF\Templates";
        private string _project_name = "New project";                                                                                           //Default project name.
        public string Project_name                                                                                                              //Accepts changes in project name then triggers OPC.
        {
            get => _project_name;
            set
            {
                if (_project_name != value)
                {
                    _project_name = value;
                    Project_path_validation();
                    OnPropertyChanged(nameof(Project_name));                                                                                    //OPC - OnPropertyChanged is a method used to raise the PropertyChanged event, signaling that a property of an object has changed, typically in data-binding scenarios to update the user interface.
                }
            }
        }

        private string _project_path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\GERLIEGameEngine\";                //Default project path.
        public string Project_path                                                                                                              //Accepts changes in project path then triggers OPC.
        {
            get => _project_path;
            set
            {
                if (_project_path != value)
                {
                    _project_path = value;
                    Project_path_validation();
                    OnPropertyChanged(nameof(Project_path));                                                                                    //OPC - OnPropertyChanged is a method used to raise the PropertyChanged event, signaling that a property of an object has changed, typically in data-binding scenarios to update the user interface.
                }   
            }
        }

        private bool _is_valid;
        public bool Is_valid
        {
            get => _is_valid;
            set
            {
                if (_is_valid != value)
                {
                    _is_valid = value;
                    OnPropertyChanged(nameof(Is_valid));   
                }
            }
        }

        private string _error_message;
        public string Error_message
        {
            get => _error_message;
            set
            {
                if (_error_message != value)
                {
                    _error_message = value;
                    OnPropertyChanged(nameof(Error_message));
                }
            }
        }

        private ObservableCollection<Class_for_project_templates> _project_templates = new ObservableCollection<Class_for_project_templates>(); //Defines a private ObservableCollection of Class_for_project_templates objects named _project_templates and provides a public property Project_templates_getter that exposes a read-only view of this collection as a ReadOnlyObservableCollection.
        public ReadOnlyObservableCollection<Class_for_project_templates> Project_templates_getter                                               //Defines a private ObservableCollection of Class_for_project_templates objects named _project_templates and provides a public property Project_templates_getter that exposes a read-only view of this collection as a ReadOnlyObservableCollection.
        {                                                                                                                                       
            get;
        }
        private bool Project_path_validation()                                                                                                  //Checks and ensures that Project_name and Project_path strings are valid. Returns error appropriate error messages when applicable.
        {
            var path = Project_path;

            if (!Path.EndsInDirectorySeparator(path))
                path += @"\";

            path += $@"{Project_name}\";

            Is_valid = false;

            if (string.IsNullOrWhiteSpace(Project_name.Trim()))
                Error_message = "Please enter a valid project name.";
            else if (Project_name.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                Error_message = "Please do not use an invalid character for the project name.";
            else if (string.IsNullOrWhiteSpace(Project_path.Trim()))
                Error_message = "Please enter a valid project path.";
            else if (Project_path.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                Error_message = "Please do not use an invalid character for the project path.";
            else if (Directory.Exists(path) && Directory.EnumerateFileSystemEntries(path).Any())
                Error_message = "Selected project folder already exists. Please choose another project path or use the existing one.";
            else
            {
                Error_message = string.Empty;
                Is_valid = true;
            }

            return Is_valid;
        }
        public string Create_project(Class_for_project_templates templates)                                                                     //Creates a project directory structure based on provided templates, sets the "Hidden" attribute for a specific subfolder, and copies icon and screenshot files for UI purposes before returning an empty string, but returns an empty string in case of any exceptions.
        {
            Project_path_validation();
            if (!Is_valid)                                                                                                                      
                return string.Empty;

            if (!Path.EndsInDirectorySeparator(Project_path))
                Project_path += @"\";

            var path = $@"{Project_path}{Project_name}\";

            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                foreach (var folder in templates.Folders)
                    Directory.CreateDirectory(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(path), folder)));

                var directory_info = new DirectoryInfo(path + @".gerlie");                                                                      //Appends .gerlie extension to the project file name.
                directory_info.Attributes |= FileAttributes.Hidden;                                                                             //Adds hidden attribute to the folder using the bitwise OR modifier.
                File.Copy(templates.Icon_file_path, Path.GetFullPath(Path.Combine(directory_info.FullName, "Icon.ICO")));                       //Fetches icon file path for UI purposes.
                File.Copy(templates.Screenshot_file_path, Path.GetFullPath(Path.Combine(directory_info.FullName, "Screenshot.png")));           //Fetches screenshot file path for UI purposes.

                var project_xml = File.ReadAllText(templates.Project_file_path);
                project_xml = string.Format(project_xml, Project_name, Project_path);
                var project_path = Path.GetFullPath(Path.Combine(path, $"{Project_name}{Class_for_central_data_structure.Extension}"));
                File.WriteAllText(project_path, project_xml);

                return path; 
            }
            catch (Exception ex) 
            { 
                Debug.WriteLine(ex.Message);
                return string.Empty;
            }
            
        }

        public Class_for_new_projects()                                                                                                         //This code defines a constructor for the Class_for_new_projects class, which initializes the Project_templates_getter property with a read-only view of a private _project_templates collection. It then attempts to find and deserialize Class_for_project_templates objects from files named "template.xml" within a specified directory _template_path, with debugging checks in place, and logs any exceptions.
        {
            Project_templates_getter = new ReadOnlyObservableCollection<Class_for_project_templates> (_project_templates);
            try
            {
                var assert_template = Directory.GetFiles(_template_path, "template.xml", SearchOption.AllDirectories);
                Debug.Assert(assert_template.Any());
                foreach (var file in assert_template)
                {
                    var template = Class_for_serialization.From_file_method<Class_for_project_templates>(file);                                 //Reads and deserializes a Class_for_project_templates object from a file named file and assigns it to the variable template.
                    
                    template.Icon_file_path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), "Icon.ICO"));
                    template.Icon = File.ReadAllBytes(template.Icon_file_path);
                    template.Screenshot_file_path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), "Screenshot.png"));
                    template.Screenshot = File.ReadAllBytes(template.Screenshot_file_path);
                    template.Project_file_path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), template.Project_file));

                    _project_templates.Add(template);
                }
                Project_path_validation();
            }   
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

}
