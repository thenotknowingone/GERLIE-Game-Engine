using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using GERLIE_Editor.Utilities;
using System.IO;

namespace GERLIE_Editor.Engine_assets
{
    [DataContract]
    public class Project_template
    {
        [DataMember]
        public string Project_type { get; set; }
        [DataMember]
        public string Project_file { get; set; }
        [DataMember]
        public List<string> Folders { get; set; }
        public byte[] Icon { get; set; }
        public byte[] Screenshot { get; set; }
        public string Icon_file_path { get; set; }
        public string Screenshot_file_path { get; set; }
        public string Project_file_path { get; set; }
    }

    class New_project : View_model_base
    {
        private readonly string _template_path = @"C:\Users\Ryck\source\repos\GERLIE\GERLIE Editor\Project Templates";
        private string _project_name = "new_project";

        public string Project_name
        {
            get => _project_name;
            set
            {
                if (_project_name != value)
                {
                    _project_name = value;
                    Validate_project_path();
                    OnPropertyChanged(nameof(Project_name));
                }
            }
        }

        private string _project_path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\GERLIEGameEngine\";
        public string Project_path
        {
            get => _project_path;
            set
            {
                if (_project_path != value)
                {
                    _project_path = value;
                    Validate_project_path();
                    OnPropertyChanged(nameof(Project_path));
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

        private ObservableCollection<Project_template> _project_templates = new ObservableCollection<Project_template>();
        public ReadOnlyObservableCollection<Project_template> Project_templates
        {
            get;
        }
        private bool Validate_project_path()
        {
            var path = Project_path;
            if (!Path.EndsInDirectorySeparator(path)) path += @"\";
            path += $@"{ Project_name}\";

            Is_valid = false;

            if(string.IsNullOrWhiteSpace(Project_name.Trim()))
            {
                Error_message = "A project name is required";
            }
            else if (Project_name.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                Error_message = "Invalid character(s) were used in the input. Please try again.";
            }
            else if (string.IsNullOrWhiteSpace(Project_path.Trim()))
            {
                Error_message = "Please select a proper project folder.";
            }
            else if (Project_path.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                Error_message = "Invalid character(s) were used in the input. Please try again.";
            }
            else if (Directory.Exists(path) && Directory.EnumerateFileSystemEntries(path).Any())
            {
                Error_message = "Selected project folder already exists and is not empty. Please utilize that folder or create a new one.";
            }
            else
            {
                Error_message = string.Empty;
                Is_valid = true;
            }

            return Is_valid;
        }

        public string Create_project(Project_template template)
        {
            Validate_project_path();
            if (!Is_valid)
            {
                return string.Empty;
            }

            if (!Path.EndsInDirectorySeparator(Project_path)) Project_path += @"\";
            var path = $@"{Project_path}{Project_name}\";

            try
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                foreach (var folder in template.Folders) 
                {
                    Directory.CreateDirectory(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(path), folder)));    
                }
                var dirInfo = new DirectoryInfo(path + @".GERLIE\");
                dirInfo.Attributes |= FileAttributes.Hidden;
                File.Copy(template.Icon_file_path, Path.GetFullPath(Path.Combine(dirInfo.FullName, "Icon.ICO")));
                File.Copy(template.Screenshot_file_path, Path.GetFullPath(Path.Combine(dirInfo.FullName, "Screenshot.png")));

                var project_xml = File.ReadAllText(template.Project_file_path);
                project_xml = string.Format(project_xml, Project_name, Project_path);
                var project_path = Path.GetFullPath(Path.Combine(path, $"{Project_name}{Project_class.Extension}"));
                File.WriteAllText(project_path, project_xml);

                return path;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        public New_project()
        {
            Project_templates = new ReadOnlyObservableCollection<Project_template>(_project_templates);

            try
            {
                var template_files = Directory.GetFiles(_template_path, "123.xml", SearchOption.AllDirectories);
                Debug.Assert(template_files.Any());

                foreach (var file in template_files)
                {
                    var template = Serializer.From_file<Project_template>(file);
                    template.Icon_file_path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), "Icon.ICO"));
                    template.Icon = File.ReadAllBytes(template.Icon_file_path);
                    template.Screenshot_file_path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), "Screenshot.png"));
                    template.Screenshot = File.ReadAllBytes(template.Screenshot_file_path);
                    template.Project_file_path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), template.Project_file));

                    _project_templates.Add(template);
                }
                Validate_project_path();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
    }
}    

