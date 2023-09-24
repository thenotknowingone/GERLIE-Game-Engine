using System.Windows;
using System.Windows.Controls;

namespace GERLIE_Editor.Engine_assets
{
    public partial class New_project_user_control : UserControl
    {
        public New_project_user_control()
        {
            InitializeComponent();
        }

        private void On_create_button_click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as New_project;
            var project_path = vm.Create_project(template_list_box.SelectedItem as Project_template);
            bool dialog_result = false;
            var win = Window.GetWindow(this);

            if (!string.IsNullOrEmpty(project_path))
            {
                dialog_result = true;
                var project = Open_project_class.Open(new Project_data() { Project_name = vm.Project_name, Project_path = project_path });
                win.DataContext = project;
            }
            win.DialogResult = dialog_result;
            win.Close();
        }
    }
}
