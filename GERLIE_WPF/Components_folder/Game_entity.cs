using GERLIE_WPF.Components_folder;
using GERLIE_WPF.Engine_assets;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace GERLIE_WPF.Components
{
    [DataContract]
    [KnownType(typeof(Class_for_transform))]
    public class Class_for_game_entity : View_model_base
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

        [OnDeserialized]
        void On_deserialized(StreamingContext context)
        {
            if (_component != null)
            {
                Components = new ReadOnlyObservableCollection<Class_for_component>(_component);
                OnPropertyChanged(nameof(Components));
            }
        }
        public Class_for_game_entity(Class_for_scenes scene)
        {
            Debug.Assert (scene != null);
            Parent_scene = scene;
            _component.Add(new Class_for_transform(this));
        }
    }
}
