using System.Windows.Controls;

namespace GERLIE_WPF.Utilities
{
    public partial class Logger_user_control : UserControl
    {
        public Logger_user_control()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                Class_for_logger.Log(Message_type.Info, "Information message");
                Class_for_logger.Log(Message_type.Warning, "Warning message");
                Class_for_logger.Log(Message_type.Error, "Error message");
            };
        }

        private void On_clear_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Class_for_logger.Clear();
        }

        private void On_message_filter_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var filter = 0x0;

            if (toggle_info.IsChecked == true)
                filter |= (int)Message_type.Info;

            if (toggle_warnings.IsChecked == true)
                filter |= (int)Message_type.Warning;

            if (toggle_errors.IsChecked == true)
                filter |= (int)Message_type.Error;

            Class_for_logger.Set_message_filter(filter);
        }
    }
}
