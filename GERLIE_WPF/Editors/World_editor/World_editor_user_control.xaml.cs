using GERLIE_WPF.Engine_assets;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace GERLIE_WPF.Editors
{
    /// <summary>
    /// Interaction logic for World_editor_user_control.xaml
    /// </summary>
    public partial class World_editor_user_control : UserControl
    {
        public World_editor_user_control()
        {
            InitializeComponent();
            Loaded += On_world_editor_view_loaded;
        }
        private void On_world_editor_view_loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= On_world_editor_view_loaded;
            Focus();
            ((INotifyCollectionChanged)Class_for_central_data_structure.Undo_Redo.Undo_list).CollectionChanged += (s, e) => Focus();
        }
    }
}
