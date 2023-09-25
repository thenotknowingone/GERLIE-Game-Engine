using GERLIE_WPF.Engine_assets;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GERLIE_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()                                                     //The first-ever function to load.
        {
            InitializeComponent();
            Loaded += Main_window_loaded;                                       //Loading Main_window_loaded.
        }
        private void Main_window_loaded(object sender, RoutedEventArgs e)       //Called when main window is loaded.
        {
            Loaded -= Main_window_loaded;                                       //Unsubscribing (removing) the Main_window_loaded event handler from the Loaded event.
            Load_project_browser();                                             //Load project browser (1).
        }
        private void Load_project_browser()                                     //Load project browser (2).
        {
            var browser = new Project_browser_window();
            if (browser.ShowDialog() == false)                                  //Whenever false, appplication closes.
            {
                Application.Current.Shutdown();
            }
            else
            {

            }
        }

    }
}
