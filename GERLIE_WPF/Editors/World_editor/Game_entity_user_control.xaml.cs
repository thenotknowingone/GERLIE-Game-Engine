using GERLIE_WPF.Components;
using GERLIE_WPF.Engine_assets;
using GERLIE_WPF.Utilities;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace GERLIE_WPF.Editors
{
    public partial class Game_entity_user_control : UserControl
    {
        private Action _undo_action;
        private string _property_name; 
        public static Game_entity_user_control Instance 
        { 
            get; 
            private set;  
        }
        public Game_entity_user_control()
        {
            InitializeComponent();
            DataContext = null;
            Instance = this;
            DataContextChanged += (_, __) =>
            {
                if (DataContext != null)
                    (DataContext as Class_for_MS_entity).PropertyChanged += (s, e) => _property_name = e.PropertyName;
            };
        }
        private Action Get_rename_action()
        {
            var vm = DataContext as Class_for_MS_entity;
            var selection = vm.Selected_entities.Select(entity => (entity, entity.Name)).ToList();
            return new Action(() =>
            {
                selection.ForEach(item => item.entity.Name = item.Name);
                (DataContext as Class_for_MS_entity).Refresh();
            });
        }
        private Action Get_is_enabled_action()
        {
            var vm = DataContext as Class_for_MS_entity;
            var selection = vm.Selected_entities.Select(entity => (entity, entity.Is_enabled)).ToList();
            return new Action(() =>
            {
                selection.ForEach(item => item.entity.Is_enabled = item.Is_enabled);
                (DataContext as Class_for_MS_entity).Refresh();
            });
        }
        private void On_name_text_box_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _undo_action = Get_rename_action();
        }
        private void On_name_text_box_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if(_property_name == nameof(Class_for_MS_entity.Name) && _undo_action != null)
            {
                var redo_action = Get_rename_action();

                Class_for_central_data_structure.Undo_Redo.Add(new Class_for_undo_redo_action(_undo_action, redo_action, "Renamed game entity."));
                _property_name = null;
            }

            _undo_action = null;
        }

        private void On_is_enabled_checkbox_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var undo_action = Get_is_enabled_action();
            var vm = DataContext as Class_for_MS_entity;
            vm.Is_enabled = (sender as CheckBox).IsChecked == true;
            var redo_action = Get_is_enabled_action();
            Class_for_central_data_structure.Undo_Redo.Add(new Class_for_undo_redo_action(undo_action, redo_action, vm.Is_enabled == true ? "Enabled game entity." : "Disabled game entity."));
        }
    }
}
