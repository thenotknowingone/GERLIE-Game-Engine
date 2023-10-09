using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace GERLIE_WPF.Utilities
{
    enum Message_type
    {
        Info = 0x01,
        Warning = 0x02,
        Error = 0x04,
    }

    class Class_for_log_message
    {
        public DateTime Time
        {
            get;
        }
        public Message_type Message_type 
        { 
            get; 
        }
        public string Message
        {
            get;
        }
        public string File
        {
            get;
        }
        public string Caller
        {
            get;
        }
        public int Line
        {
            get;
        }
        public string Meta_data => $"{File} : {Caller} ({Line})";

        public Class_for_log_message(Message_type type, string message, string file, string caller, int line)
        {
            Time = DateTime.Now;
            Message_type = type;
            Message = message; 
            File = Path.GetFileName(file);
            Caller = caller; 
            Line = line;
        }
    }

    static class Class_for_logger
    {
        private static int _message_filter = (int)(Message_type.Info | Message_type.Warning | Message_type.Error);
        private static readonly ObservableCollection<Class_for_log_message> _messages = new ObservableCollection<Class_for_log_message>();
        public static ReadOnlyObservableCollection<Class_for_log_message> Messages
        {
            get;
        } = new ReadOnlyObservableCollection<Class_for_log_message> (_messages);
        public static CollectionViewSource Filtered_messages
        {
            get;    
        } = new CollectionViewSource() { Source = Messages };
        public static async void Log(Message_type type, string message,
            [CallerFilePath]string file = "", [CallerMemberName]string caller="",
            [CallerLineNumber]int line = 0)
        {
            await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                _messages.Add(new Class_for_log_message(type, message, file, caller, line));
            }));
        }

        public static async void Clear()
        {
            await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                _messages.Clear();
            }));
        }
        public static void Set_message_filter(int mask)
        {
            _message_filter = mask;
            Filtered_messages.View.Refresh();
        }
        static Class_for_logger()
        {
            Filtered_messages.Filter += (s, e) =>
            {
                var type = (int)(e.Item as Class_for_log_message).Message_type;
                e.Accepted = (type & _message_filter) != 0;
            };
        }
    }
}
