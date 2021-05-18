using System.Xml.Serialization;

namespace Menu.SettingsGame
{
    [XmlRoot("settings")]
    public class Settings
    {
        [XmlElement("details")] public int levelOfDetails { get; set; }
        [XmlElement("resolution")] public int resolutions { get; set; }
        [XmlElement("fullscreen")] public bool fullscreen { get; set; }
        [XmlElement("volume")] public int volume { get; set; }
        [XmlElement("voice-support")] public bool speechOutputSupport { get; set; }
    }
}
