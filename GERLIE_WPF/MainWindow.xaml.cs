using GERLIE_WPF.Engine_assets;
using System.ComponentModel;
using System.Windows;

namespace GERLIE_WPF
{ 
    public partial class MainWindow : Window
    {
        public MainWindow()                                                     //The first-ever function to load.
        {
            InitializeComponent();
            Loaded += Main_window_loaded;                                       //Loading Main_window_loaded.
            Closing += Main_window_closing;
        }
        private void Main_window_loaded(object sender, RoutedEventArgs e)       //Called when main window is loaded.
        {
            Loaded -= Main_window_loaded;                                       //Unsubscribing (removing) the Main_window_loaded event handler from the Loaded event.
            Load_project_browser();                                             //Load project browser (1).
        }
        private void Main_window_closing(object sender, CancelEventArgs e)
        {
            Closing -= Main_window_closing;
            Class_for_central_data_structure.Current?.Unload();
        }
        private void Load_project_browser()                                     //Load project browser (2).
        {
            var browser = new Project_browser_window();

            if (browser.ShowDialog() == false || browser.DataContext == null)   //Whenever false, appplication closes.
                Application.Current.Shutdown();
            else
            {
                Class_for_central_data_structure.Current?.Unload();
                DataContext = browser.DataContext;
            }
        }

    }
}
