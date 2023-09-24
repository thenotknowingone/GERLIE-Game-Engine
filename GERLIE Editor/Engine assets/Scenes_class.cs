using System.Diagnostics;
using System.Runtime.Serialization;

namespace GERLIE_Editor.Engine_assets
{
    [DataContract]
    public class Scenes_class : View_model_base
    {
        private string _name;
        [DataMember]
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        [DataMember]
        public Project_class Project
        {
            get;
            private set;
        }
        private bool _is_active;
        [DataMember]
        public bool Is_active
        {
            get => _is_active;
            set
            {
                if (_is_active != value) 
                {
                    _is_active = value;
                    OnPropertyChanged(nameof(Is_active));
                }
            }
        }
        public Scenes_class(Project_class project, string name) 
        {
            Debug.Assert(project != null);
            Project = project;
            Name = name;
        }
    }
}
