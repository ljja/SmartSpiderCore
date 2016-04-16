using System.IO;
using System.Xml.Serialization;

namespace SmartSpiderCore
{
    public class ConfigManage
    {
        private static readonly ConfigManage _instance = new ConfigManage();

        public static ConfigManage Instance
        {
            get { return _instance; }
        }
        public T Load<T>(string path) where T : class
        {
            if (!File.Exists(path))
            {
                Save(path, default(T));
            }

            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var read = new StreamReader(path))
            {
                return xmlSerializer.Deserialize(read) as T;
            }
        }

        public void Save<T>(string path, T t) where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var write = new StreamWriter(path))
            {
                xmlSerializer.Serialize(write, t);
            }
        }

    }
}
