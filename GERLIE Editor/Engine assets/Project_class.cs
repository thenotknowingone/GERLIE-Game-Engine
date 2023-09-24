using GERLIE_Editor.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;

namespace GERLIE_Editor.Engine_assets
{
    [DataContract(Name = "Game")] 
    public class Project_class : View_model_base
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

        [DataMember(Name = "Scenes")]
        private ObservableCollection<Scenes_class> _scenes = new ObservableCollection<Scenes_class>();
        public ReadOnlyObservableCollection<Scenes_class> Scenes
        {
            get; private set;
        }

        private Scenes_class _active_scene;
        public Scenes_class active_scene
        {
            get => _active_scene;
            set
            {
                if (_active_scene != value) 
                { 
                    _active_scene = value;
                    OnPropertyChanged(nameof(active_scene));
                }
            }
        }

        public static Project_class Current => Application.Current.MainWindow.DataContext as Project_class;

        public static Project_class Load(string file)
        {
            Debug.Assert(File.Exists(file));
            return Serializer.From_file<Project_class>(file);
        }
        public void Unload()
        {

        }
        public static void Save(Project_class project)
        {
            Serializer.To_file(project, project.Full_path);
        }

        [OnDeserialized]
        private void On_deserialized(StreamingContext context) 
        {
            if(_scenes != null)
            {
                Scenes = new ReadOnlyObservableCollection<Scenes_class>(_scenes);
                OnPropertyChanged(nameof(Scenes));
            }
            active_scene = Scenes.FirstOrDefault(x => x.Is_active);
        }
        public Project_class(string name, string path) 
        {
            Name = name;
            Path = path;

            On_deserialized(new StreamingContext());
        }
    }
}
