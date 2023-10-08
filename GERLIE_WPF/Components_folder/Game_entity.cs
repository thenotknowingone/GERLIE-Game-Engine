using GERLIE_WPF.Components_folder;
using GERLIE_WPF.Engine_assets;
using GERLIE_WPF.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Windows.Input;

namespace GERLIE_WPF.Components
{
    [DataContract]
    [KnownType(typeof(Class_for_transform))]
    public class Class_for_game_entity : View_model_base
    {
        private bool _is_enabled = true;

        [DataMember]
        public bool Is_enabled
        {
            get => _is_enabled;
            set
            {
                if(_is_enabled !=value)
                {
                    _is_enabled = value;
                    OnPropertyChanged(nameof(Is_enabled));
                }
            }
        }

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
        public Class_for_scenes Parent_scene
        {
            get;
            private set;
        }

        [DataMember(Name = nameof(Components))]
        private readonly ObservableCollection<Class_for_component> _component = new ObservableCollection<Class_for_component>();

        public ReadOnlyObservableCollection<Class_for_component> Components
        {
            get;
            private set;
        }

        public ICommand Rename_command
        {
            get;
            set;
        }
        public ICommand Enable_command
        {
            get;
            set;
        }

        [OnDeserialized]
        void On_deserialized(StreamingContext context)
        {
            if (_component != null)
            {
                Components = new ReadOnlyObservableCollection<Class_for_component>(_component);
                OnPropertyChanged(nameof(Components));
            }

            Rename_command = new Class_for_relay_command<string>(x =>
            {
                var old_name = _name;
                Name = x;
                Class_for_central_data_structure.Undo_Redo.Add(new Class_for_undo_redo_action(nameof(Name), this, old_name, x, $"Rename entity '{old_name} to '{x}'"));
            }, x => x!= _name);
        }
        public Class_for_game_entity(Class_for_scenes scene)
        {
            Debug.Assert (scene != null);
            Parent_scene = scene;
            _component.Add(new Class_for_transform(this));
            On_deserialized(new StreamingContext());
        }
    }
}
