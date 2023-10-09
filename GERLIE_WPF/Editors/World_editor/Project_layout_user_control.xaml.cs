using GERLIE_WPF.Components;
using GERLIE_WPF.Engine_assets;
using GERLIE_WPF.Utilities;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GERLIE_WPF.Editors
{
    public partial class Project_layout_user_control : UserControl
    {
        public Project_layout_user_control()
        {
            InitializeComponent();
        }

        private void On_add_game_entity_Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var vm = btn.DataContext as Class_for_scenes;
            vm.Add_game_entity_command.Execute(new Class_for_game_entity(vm) { Name = "Empty game entity" });
        }

        private void On_game_entity_list_box_Selection_Changed(object sender, SelectionChangedEventArgs e)
        {
            var list_box = sender as ListBox;           
            var new_selection = list_box.SelectedItems.Cast<Class_for_game_entity>().ToList();
            var previous_selection = new_selection.Except(e.AddedItems.Cast<Class_for_game_entity>()).Concat(e.RemovedItems.Cast<Class_for_game_entity>()).ToList();

            Class_for_central_data_structure.Undo_Redo.Add(new Class_for_undo_redo_action(
                () =>
                {
                    list_box.UnselectAll();
                    previous_selection.ForEach(x => (list_box.ItemContainerGenerator.ContainerFromItem(x) as ListBoxItem).IsSelected = true);
                },
                () =>
                {
                    list_box.UnselectAll();
                    new_selection.ForEach(x => (list_box.ItemContainerGenerator.ContainerFromItem(x) as ListBoxItem).IsSelected = true);
                },

                "Selection changed."

                ));

            Class_for_MS_entity ms_entity = null;

            if(new_selection.Any())
                ms_entity = new Class_for_MS_game_entity(new_selection);

            Game_entity_user_control.Instance.DataContext = ms_entity;
        }
    }
}
