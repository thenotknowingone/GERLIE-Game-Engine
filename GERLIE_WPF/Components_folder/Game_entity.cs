using GERLIE_WPF.Engine_assets;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace GERLIE_WPF.Components
{
    [DataContract]
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

        [DataMember(Name = nameof(Component))]
        private readonly ObservableCollection<Class_for_component> _component = new ObservableCollection<Class_for_component>();
        public ReadOnlyObservableCollection<Class_for_component> Component
        {
            get;
        }
        public Class_for_game_entity(Class_for_scenes scene)
        {
            Debug.Assert (scene != null);
            Parent_scene = scene;
        }
    }
}
