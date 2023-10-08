using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace GERLIE_WPF.Dictionary
{
    public partial class Control_templates : ResourceDictionary
    {
        private void On_text_box_KeyDown(object sender, KeyEventArgs e)
        {
            var text_box = sender as TextBox;
            var exp = text_box.GetBindingExpression(TextBox.TextProperty);

            if (exp != null) 
                return;

            if (e.Key == Key.Enter)
            {
                if (text_box.Tag is ICommand command && command.CanExecute(text_box.Text))
                    command.Execute(text_box.Text);

                else
                    exp.UpdateSource();
                
                Keyboard.ClearFocus();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                exp.UpdateTarget(); 
                Keyboard.ClearFocus();
            }
        }
    }
}
