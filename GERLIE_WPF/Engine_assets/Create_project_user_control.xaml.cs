using System.Windows.Controls;
using System.Windows;

namespace GERLIE_WPF.Engine_assets
{
    /// <summary>
    /// Interaction logic for Create_project_user_control.xaml
    /// </summary>
    public partial class Create_project_user_control : UserControl
    {
        public Create_project_user_control()
        {
            InitializeComponent();
        }

        private void On_create_Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as Class_for_new_projects;
            
            var project_path = vm.Create_project(template_list_box.SelectedItem as Class_for_project_templates);

            bool dialog_result = false;

            var win = Window.GetWindow(this);

            if (!string.IsNullOrEmpty(project_path)) 
                dialog_result = true;

            win.DialogResult = dialog_result;
            win.Close();
        }
    }
}
