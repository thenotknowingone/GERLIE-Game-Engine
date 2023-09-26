using GERLIE_WPF.Common;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace GERLIE_WPF.Engine_assets
{
    [DataContract(Name = "Game")]
    class Class_for_central_data_structure : View_model_base
    {
        public static string Extension
        {
            get;
        } = ".gerlie";
        [DataMember]
        public string Name
        {
            get;
            private set;
        }
        [DataMember]
        public string Path
        {
            get;
            private set;
        }
        public string Full_path => $"{Path}{Name}{Extension}";

        [DataMember (Name  = "Scenes")]
        private ObservableCollection<Class_for_scenes> _scene = new ObservableCollection<Class_for_scenes> ();
        public ReadOnlyObservableCollection<Class_for_scenes> Scene
        {
            get;
        }

        public Class_for_central_data_structure(string name, string path)
        {
            Name = name;
            Path = path;

            _scene.Add(new Class_for_scenes(this, "Default scene"));
        }
    }
}
