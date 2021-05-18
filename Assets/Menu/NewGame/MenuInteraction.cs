using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using AIMLbot.AIMLTagHandlers;
using UnityEngine.Rendering;

namespace Menu.NewGame
{
    [XmlRoot("mainMenu")]
    public class MenuInteraction
    {
        [XmlElement("newGame")]
        public NewGame newGame { get; set; }

        [XmlElement("save-info")]
        public SaveInfo saveInfo { get; set; }

        [XmlElement("dialogue")]
        public List<DialogueHistory> dialogueHistories { get; set; }

        public MenuInteraction()
        {
            newGame = new NewGame();
            saveInfo = new SaveInfo();
        }

    }

    public class NewGame
    {
        [XmlElement("gender")] public int gender{ get; set; }
        [XmlElement("inputType")] public int inputType{ get; set; }
        [XmlElement("name")] public string name{ get; set; }
    }

    public class SaveInfo
    {
        [XmlElement("date-time")] public string DateTime{ get; set; }
        [XmlElement("mood")] public int mood { get; set; }
    }

    public class DialogueHistory
    {
        [XmlElement("input")]
        public string input { get; set; }

        [XmlElement("output")]
        public string output { get; set; }
    }
}
