using System.Runtime.Serialization.Json;

namespace BBEngine.Level.Serialization
{
    public static class serializer
    {
        public static void to_file<T>(T instance, string path)
        {
            using FileStream fs = new(path, FileMode.Create, FileAccess.Write);
            var serializer = new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(fs, instance);
        }

        public static T? from_file<T>(string path)
        {
            try
            {
                using FileStream fs = new(path, FileMode.Open, FileAccess.Read);
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(fs);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
