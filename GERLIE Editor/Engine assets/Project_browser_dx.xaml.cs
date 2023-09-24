using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GERLIE_Editor.Engine_assets
{
    /// <summary>
    /// Interaction logic for Project_browser_dx.xaml
    /// </summary>
    public partial class Project_browser_dx : Window
    {
        public Project_browser_dx()
        {
            InitializeComponent();
        }
        private void On_toggle_click(object sender, RoutedEventArgs e)
        {
            if (sender == Open_existing_project_button)
            {
                if(new_project_button.IsChecked == true)
                {
                    new_project_button.IsChecked = false;
                    browser_cx.Margin = new Thickness(0);
                }
                Open_existing_project_button.IsChecked = true;
            }
            else
            {
                if (Open_existing_project_button.IsChecked == true)
                {
                    Open_existing_project_button.IsChecked = false;
                    browser_cx.Margin = new Thickness(-800, 0, 0, 0);
                }
                new_project_button.IsChecked = true;
            }
        }
    }
}
