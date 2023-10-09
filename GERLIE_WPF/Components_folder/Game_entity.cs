using GERLIE_WPF.Components_folder;
using GERLIE_WPF.Engine_assets;
using GERLIE_WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Input;

namespace GERLIE_WPF.Components
{
    [DataContract]
    [KnownType(typeof(Class_for_transform))]
    class Class_for_game_entity : View_model_base
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
        private readonly ObservableCollection<Class_for_component> _components = new ObservableCollection<Class_for_component>();

        public ReadOnlyObservableCollection<Class_for_component> Components
        {
            get;
            private set;
        }
        [OnDeserialized]
        void On_deserialized(StreamingContext context)
        {
            if (_components != null)
            {
                Components = new ReadOnlyObservableCollection<Class_for_component>(_components);
                OnPropertyChanged(nameof(Components));
            }                        
        }
        public Class_for_game_entity(Class_for_scenes scene)
        {
            Debug.Assert (scene != null);
            Parent_scene = scene;
            _components.Add(new Class_for_transform(this));
            On_deserialized(new StreamingContext());
        }
    }

    abstract class Class_for_MS_entity : View_model_base
    {
        private bool _enable_updates = true;                                                                                                                                                //Enables updates to selected entities.
        private bool? _is_enabled;                                                                                                                                                          //Nullable.
        public bool? Is_enabled                                                                                                                                                             //Nullable.
        {
            get => _is_enabled;
            set
            {
                if (_is_enabled != value)
                {
                    _is_enabled = value;
                    OnPropertyChanged(nameof(Is_enabled));
                }
            }
        }

        private string _name;
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

        private readonly ObservableCollection<IMS_component> _components = new ObservableCollection<IMS_component>();
        public ReadOnlyObservableCollection<IMS_component> Components
        {
            get;
        }
        public List<Class_for_game_entity> Selected_entities
        {
            get;
        }        
        public static float? Get_mixed_value(List<Class_for_game_entity> entities, Func<Class_for_game_entity, float> get_property)
        {
            var value = get_property(entities.First());

            foreach (var entity in entities.Skip(1))
            {
                if (!value.Is_the_same_as(get_property(entity)))
                    return null;
            }

            return value;
        }
        public static bool? Get_mixed_value(List<Class_for_game_entity> entities, Func<Class_for_game_entity, bool> get_property)
        {
            var value = get_property(entities.First());

            foreach (var entity in entities.Skip(1))
            {
                if (value != (get_property(entity)))
                {
                    return null;
                }
            }

            return value;
        }
        public static string Get_mixed_value(List<Class_for_game_entity> entities, Func<Class_for_game_entity, string> get_property)
        {
            var value = get_property(entities.First());

            foreach (var entity in entities.Skip(1))
            {
                if (value != (get_property(entity)))
                {
                    return null;
                }
            }

            return value;
        }
        protected virtual bool Update_game_entities(string property_name)
        {
            switch (property_name)
            {
                case nameof(Is_enabled):
                    Selected_entities.ForEach(x => x.Is_enabled = Is_enabled.Value);
                    return true;
                case nameof(Name):
                    Selected_entities.ForEach(x => x.Name = Name);
                    return true;
            }
            return false;
        }
        protected virtual bool Update_MS_game_entity()
        {
            Is_enabled = Get_mixed_value(Selected_entities, new Func<Class_for_game_entity, bool>(x => x.Is_enabled));
            Name = Get_mixed_value(Selected_entities, new Func<Class_for_game_entity, string>(x => x.Name));

            return true;
        }
        public void Refresh()
        {
            _enable_updates = false;
            Update_MS_game_entity();
            _enable_updates = true;
        }
        public Class_for_MS_entity(List<Class_for_game_entity> entities) 
        {
            Debug.Assert(entities?.Any() == true);
            Components = new ReadOnlyObservableCollection<IMS_component>(_components);
            Selected_entities = entities;
            PropertyChanged += (s, e) => 
            { 
                if(_enable_updates)
                    Update_game_entities(e.PropertyName); 
            };
        }
    }
    class Class_for_MS_game_entity : Class_for_MS_entity
    {
        public Class_for_MS_game_entity(List<Class_for_game_entity> entities) : base(entities)
        {
            Refresh();
        }
    }
}
