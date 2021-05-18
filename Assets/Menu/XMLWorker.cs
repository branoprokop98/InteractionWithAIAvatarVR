using System;
using System.IO;
using System.Xml.Serialization;

namespace Menu
{
    public class XMLWorker
    {
        public static void serialize(object item, string path)
        {
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            StreamWriter writer = new StreamWriter(path);
            serializer.Serialize(writer.BaseStream, item);
            writer.Close();
        }

        public static T deserialize<T>(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            StreamReader reader = new StreamReader(path);
            try
            {
                T deserialize = (T) xml.Deserialize(reader.BaseStream);
                reader.Close();
                return deserialize;
            }
            catch (Exception e)
            {
                reader.Close();
            }
            reader.Close();
            return default(T);
        }
    }
}
