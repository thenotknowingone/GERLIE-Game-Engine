using System.Windows;

namespace GERLIE_WPF.Engine_assets
{ 
    public partial class Project_browser_window : Window
    {
        public Project_browser_window()
        {
            InitializeComponent();
        }
        private void project_browser_Button_Click(object sender, RoutedEventArgs e)                 //Project browser windows toggle instructions.
        {
            if (sender == retrieve_project_button)
            {
                if (create_project_button.IsChecked == true)
                {
                    create_project_button.IsChecked = false;
                    browser_content.Margin = new Thickness(0);
                }
                retrieve_project_button.IsChecked = true;
            }
            else
            {
                if (retrieve_project_button.IsChecked == true)
                {
                    retrieve_project_button.IsChecked = false;
                    browser_content.Margin = new Thickness(-800, 0, 0, 0);
                }
                create_project_button.IsChecked = true;
            }
        }
    }
}
