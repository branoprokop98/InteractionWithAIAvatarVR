using System.Collections.Generic;
using System.Xml.Serialization;

namespace AIML
{
    [XmlRoot("root")]
    public class AimlSettings
    {
        [XmlElement("item")] public List<Config> settings;

        public AimlSettings()
        {
        }
    }

    public class Config
    {
        [XmlAttribute("name")]
        public string nameOfAttribute;

        [XmlAttribute("value")]
        public string valueOfAttribute;
    }
}
