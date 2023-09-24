using System.ComponentModel;
using System.Runtime.Serialization;

namespace GERLIE_Editor
{
    [DataContract(IsReference = true)]
    public class View_model_base : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
