using System;
using System.Windows.Input;

namespace GERLIE_WPF
{
    class Class_for_relay_command<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _can_execute;
     
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            return _can_execute?.Invoke((T)parameter) ?? true;
        }
        public void Execute(object parameter) 
        {
            _execute((T)parameter);
        }
        public Class_for_relay_command(Action<T> execute, Predicate<T> can_execute = null)        
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _can_execute = can_execute;
        }
    }
}
