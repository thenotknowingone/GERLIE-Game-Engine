using GERLIE_WPF.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Input;

namespace GERLIE_WPF.Engine_assets
{
    [DataContract(Name = "Game")]
    public class Class_for_central_data_structure : View_model_base
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
        public string Full_path => $@"{Path}{Name}\{Name}{Extension}";
        
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
        public static Class_for_undo_redo Undo_Redo
        {
            get;
        }= new Class_for_undo_redo();
        public ICommand Undo_command
        {
            get;
            private set;
        }
        public ICommand Redo_command
        {
            get;
            private set;
        }
        public ICommand Add_scene_command
        {
            get;
            private set;
        }
        public ICommand Remove_scene_command
        {
            get;
            private set;
        }
        public ICommand Save_command
        {
            get;
            private set;
        }
        private void Add_a_scene_internal (string scene_name)                                                                                                   //Adds a scene to the Project layout container.
        {
            _scene.Add(new Class_for_scenes(this, scene_name));
        }
        private void Remove_a_scene_internal(Class_for_scenes scene)                                                                                            //Removes a scene from the Project layout container.
        {
            _scene.Remove(scene);
        }
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

            Add_scene_command = new Class_for_relay_command<object>(x =>
            {
                Add_a_scene_internal($"New Scene {_scene.Count}");
                var new_scene = _scene.Last();
                var scene_index = _scene.Count - 1;
                Undo_Redo.Add(new Class_for_undo_redo_action(
                () => Remove_a_scene_internal(new_scene),
                () => _scene.Insert(scene_index, new_scene),
                $"Added {new_scene.Name}"));
            });

            Remove_scene_command = new Class_for_relay_command<Class_for_scenes>(x =>
            {
                var scene_index = _scene.IndexOf(x);
                Remove_a_scene_internal(x);

                Undo_Redo.Add(new Class_for_undo_redo_action(
                    () => _scene.Insert(scene_index, x),
                    () => Remove_a_scene_internal(x),
                    $"Removed {x.Name}"));
            }, x => !x.Is_active);

            Undo_command = new Class_for_relay_command<object>(x => Undo_Redo.Undo());
            Redo_command = new Class_for_relay_command<object>(x => Undo_Redo.Redo());
            Save_command = new Class_for_relay_command<object>(x => Save(this));

        }
        public Class_for_central_data_structure(string name, string path)
        {
            Name = name;
            Path = path;

            On_deserialized(new StreamingContext());
        }
    }
}
