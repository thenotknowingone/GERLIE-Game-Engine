using GERLIE_WPF.Common;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace GERLIE_WPF.Engine_assets
{
    [DataContract]
    class Class_for_scenes : View_model_base
    {
        private string _name;
        [DataMember]
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        [DataMember]
        public Class_for_central_data_structure Project
        {   
            get; 
            private set; 
        }
        public Class_for_scenes(Class_for_central_data_structure project, string name)
        {
            Debug.Assert(project != null);
                Project = project;
           
            Name = name;
        }
    }   
}
