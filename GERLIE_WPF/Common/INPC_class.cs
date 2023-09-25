using System.ComponentModel;

namespace GERLIE_WPF.Common
{
    public class View_model_base : INotifyPropertyChanged                                   //The class needed to detect changes with the UI IO. INotifyPropertyChanged is an interface in .NET used to notify the user interface of property changes in an object, enabling automatic updates of UI elements bound to those properties.
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property_name)                              //PropertyChangedEventHandler is a delegate type in .NET used to define event handlers for property change notifications, typically associated with the INotifyPropertyChanged interface in data-binding scenarios.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property_name));     //Invoke is a method in C# used to call a delegate or execute a method asynchronously, often used in multithreaded or event-driven programming.
        }
    }
}
