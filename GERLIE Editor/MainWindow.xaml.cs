using GERLIE_Editor.Engine_assets;
using System.ComponentModel;
using System.Windows;

namespace GERLIE_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Main_window_loaded;
            Closing += Main_window_exit;
        }
        private void Main_window_loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= Main_window_loaded;
            Project_browser_dx();
        }
        private void Main_window_exit(object sender, CancelEventArgs e)
        {
            Closing -= Main_window_exit;
            Project_class.Current?.Unload();
        }
        private void Project_browser_dx()
        {
            var project_browser = new Project_browser_dx();

            if (project_browser.ShowDialog() == false || project_browser.DataContext == null)
                Application.Current.Shutdown();
            else
            {
                Project_class.Current?.Unload();
                DataContext = project_browser.DataContext;
            }
        }
    }
}
