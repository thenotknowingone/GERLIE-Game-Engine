using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GERLIE_Editor.Engine_assets
{
    public partial class Existing_project_user_control : UserControl
    {
        public Existing_project_user_control()
        {
            InitializeComponent();
        }

        private void On_retrieve_button_click(object sender, RoutedEventArgs e)
        {
            Open_selected_project();
        }
        private void On_list_box_item_mouse_double_click(object sender, MouseButtonEventArgs e)
        {
            Open_selected_project();
        }
        private void Open_selected_project() 
        {
            var project = Open_project_class.Open(projects_list_box.SelectedItem as Project_data);
            bool dialog_result = false;
            var win = Window.GetWindow(this);

            if (project != null)
            {
                dialog_result = true;
                win.DataContext = project;
            }
            win.DialogResult = dialog_result;
            win.Close();
        }
    }
}
