using GERLIE_WPF.Common;
using GERLIE_WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace GERLIE_WPF.Engine_assets
{
    [DataContract]
    public class Class_for_project_templates                                                //The Class_for_project_templates defines a template for storing project-related information, including project type, associated file, and a list of folders, using properties to access and modify these values.
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
    }
    class Class_for_new_projects : View_model_base                                          //Class for new projects inheriting from INPC VM.
    {
        private readonly string _template_path = @"E:\Programming Projects\GERLIE\GERLIE_WPF\Templates";
        private string _project_name = "New project";                                       //Default project name.
        public string Project_name                                                          //Accepts changes in project name then triggers OPC.
        {
            get => _project_name;
            set
            {
                if (_project_name != value)
                {
                    _project_name = value;
                    OnPropertyChanged(nameof(Project_name));                                //OPC - OnPropertyChanged is a method used to raise the PropertyChanged event, signaling that a property of an object has changed, typically in data-binding scenarios to update the user interface.
                }
            }
        }

        private string _project_path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\GERLIEGameEngine\"; //Default project path.
        public string Project_path                                                          //Accepts changes in project path then triggers OPC.
        {
            get => _project_path;
            set
            {
                if (_project_path != value)
                {
                    _project_path = value;
                    OnPropertyChanged(nameof(Project_path));                                //OPC - OnPropertyChanged is a method used to raise the PropertyChanged event, signaling that a property of an object has changed, typically in data-binding scenarios to update the user interface.
                }
            }
        }
        public Class_for_new_projects()
        {
            try
            {
                var assert_template = Directory.GetFiles(_template_path, "template.xml", SearchOption.AllDirectories);
                Debug.Assert(assert_template.Any());
                foreach (var file in assert_template)
                {
                    var template = new Class_for_project_templates()
                    {
                        Project_type = "Empty Project",
                        Project_file = "project.gerlie",
                        Folders = new List<string>() { ".gerlie", "Content", "Game_code" }
                    };
                    Class_for_serialization.ToFile(template, file);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

}
