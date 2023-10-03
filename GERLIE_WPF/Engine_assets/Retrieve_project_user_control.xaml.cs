using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GERLIE_WPF.Engine_assets
{
    /// <summary>
    /// Interaction logic for Retrieve_project_user_control.xaml
    /// </summary>
    public partial class Retrieve_project_user_control : UserControl
    {
        public Retrieve_project_user_control()
        {
            InitializeComponent();
        }
        private void On_retrieve_Button_Click(object sender, RoutedEventArgs e)
        {
            Retrieve_project();
        }
        private void On_list_box_item_Mouse_Double_Click(object sender, MouseButtonEventArgs e)
        {
            Retrieve_project();
        }
        private void Retrieve_project()
        {
            var project = Class_for_retrieving_projects.Open(projects_list_box.SelectedItem as Class_for_project_data);
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
