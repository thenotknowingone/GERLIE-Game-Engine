using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;

namespace GERLIE_WPF.Utilities
{
    public static class Class_for_serialization
    {
        public static void ToFile<T>(T instance, string path)                           //This code defines a method ToFile that takes an object, serializes it into XML format using a DataContractSerializer, and saves it to a file specified by the path parameter.
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
            }
        }
    }
}
