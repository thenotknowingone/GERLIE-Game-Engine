using GERLIE_WPF.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;

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
        } = "New Project";
        [DataMember]
        public string Path
        {
            get;
            private set;
        }
        public string Full_path => $"{Path}{Name}{Extension}";
        
        private Class_for_scenes _current_scene;
        [DataMember]
        public Class_for_scenes Current_scene
        {
            get => _current_scene;
            set
            {
                if (_current_scene!= null)
                {
                    _current_scene = value;
                    OnPropertyChanged(nameof(Current_scene));
                }
            }
        }

        [DataMember (Name  = "Scenes")]
        private ObservableCollection<Class_for_scenes> _scene = new ObservableCollection<Class_for_scenes> ();
        public ReadOnlyObservableCollection<Class_for_scenes> Scene
        {
            get;
            private set;
        }
        public static Class_for_central_data_structure Current => Application.Current.MainWindow.DataContext as Class_for_central_data_structure;
        public static Class_for_central_data_structure Load(string file)
        {
            Debug.Assert(File.Exists(file));
            return Class_for_serialization.From_file_method<Class_for_central_data_structure>(file);
        }
        public void Unload()
        {

        }   
        public static void Save(Class_for_central_data_structure project)
        {
            Class_for_serialization.To_file_method(project, project.Full_path);
        }
        [OnDeserialized]
        private void On_deserialized(StreamingContext context)
        {
            if (_scene != null)
            {
                Scene = new ReadOnlyObservableCollection<Class_for_scenes>(_scene);
                OnPropertyChanged(nameof(Scene));
            }
            Current_scene = Scene.FirstOrDefault(x => x.Is_active);
        }
        public Class_for_central_data_structure(string name, string path)
        {
            Name = name;
            Path = path;

            On_deserialized(new StreamingContext());
        }
    }
}
