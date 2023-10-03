using GERLIE_WPF.Components;
using GERLIE_WPF.Engine_assets;
using System.Windows;
using System.Windows.Controls;

namespace GERLIE_WPF.Editors
{
    /// <summary>
    /// Interaction logic for Project_layout_user_control.xaml
    /// </summary>
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

        private void On_game_entity_Selection_Changed(object sender, SelectionChangedEventArgs e)
        {
            var entity = (sender as ListBox).SelectedItems[0];
            Class_for_game_entity.Instance.DataContext = entity;
        }
    }
}
