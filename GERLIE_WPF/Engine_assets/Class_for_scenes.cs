using GERLIE_WPF.Components;
using GERLIE_WPF.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Windows.Input;
using static System.Formats.Asn1.AsnWriter;

namespace GERLIE_WPF.Engine_assets
{
    [DataContract]
    public class Class_for_scenes : View_model_base
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

        [DataMember(Name = nameof(Game_entities))]
        private ObservableCollection<Class_for_game_entity> _game_entities = new ObservableCollection<Class_for_game_entity>();
        public ReadOnlyObservableCollection<Class_for_game_entity> Game_entities
        {
            get;
            private set;
        }
        public ICommand Add_game_entity_command
        {
            get;
            private set;
        }
        public ICommand Remove_game_entity_command
        {
            get;
            private set;
        }
        private void Add_game_entity(Class_for_game_entity entity)
        {
            Debug.Assert(!_game_entities.Contains(entity));
            _game_entities.Add(entity);
        }
        private void Remove_game_entity(Class_for_game_entity entity)
        {
            Debug.Assert(_game_entities.Contains(entity));
            _game_entities.Remove(entity);
        }

        [OnDeserialized]
        private void On_deserialized(StreamingContext context)
        {            
            if (_game_entities != null)
            {
                Game_entities = new ReadOnlyObservableCollection<Class_for_game_entity>(_game_entities);
                OnPropertyChanged(nameof(Game_entities));
            }

            Add_game_entity_command = new Class_for_relay_command<Class_for_game_entity>(x =>
            {
                Add_game_entity(x);
                var game_entity_index = _game_entities.Count - 1;
                
                Class_for_central_data_structure.Undo_Redo.Add(new Class_for_undo_redo_action(
                    () => Remove_game_entity(x),
                    () => _game_entities.Insert(game_entity_index, x),
                $"Added {x.Name} to {Name}."));
            });

            Remove_game_entity_command = new Class_for_relay_command<Class_for_game_entity>(x =>
            {
                var game_entity_index = _game_entities.IndexOf(x); 
                Remove_game_entity(x);

                Class_for_central_data_structure.Undo_Redo.Add(new Class_for_undo_redo_action(
                    () => _game_entities.Insert(game_entity_index,x),
                    () => Remove_game_entity(x),
                $"Removed {x.Name}."));
            });
        }
        public Class_for_scenes(Class_for_central_data_structure project, string name)
        {
            Debug.Assert(project != null);
                
            Project = project;
            Name = name;
            On_deserialized(new StreamingContext());
        }
    }   
}
