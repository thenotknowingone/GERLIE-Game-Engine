using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;

namespace GERLIE_WPF.Utilities
{
    public static class Class_for_serialization
    {
        public static void To_file_method<T>(T instance, string path)                           //This code defines a method To_file that takes an object, serializes it into XML format using a DataContractSerializer, and saves it to a file specified by the path parameter.
        {
            try
            {
                using var serializer_fstream = new FileStream(path, FileMode.Create);
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(serializer_fstream, instance);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Class_for_logger.Log(Message_type.Error, $"Failed to serialize {instance} to {path}.");
                throw;
            }
        }
        internal static T From_file_method<T>(string path)                                      //This code reads and deserializes an object of type T from a file specified by the path and returns it.
        {
            try
            {
                using var serializer_fstream = new FileStream(path, FileMode.Open);
                var serializer = new DataContractSerializer(typeof(T));
                T instance = (T)serializer.ReadObject(serializer_fstream);
                return instance;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Class_for_logger.Log(Message_type.Error, $"Failed to deserialize {path}.");
                throw;
            }
        }
    }
}
