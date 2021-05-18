using System;
using System.Collections.Generic;
using System.IO;
using AIML;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.NewGame
{
    public class NewGameController
    {
        private readonly MenuInteraction menuInteraction;
        private readonly Canvas canvas;
        private string pathToConfig;
        private List<Dropdown.OptionData> maleNames;
        private List<Dropdown.OptionData> femaleNames;

        public NewGameController(Canvas canvas)
        {
            menuInteraction = new MenuInteraction();
            pathToConfig = Path.Combine(Environment.CurrentDirectory, Path.Combine("config", "Settings.xml"));
            this.canvas = canvas;
            menuInteraction.saveInfo.mood = -1;
            maleNames = new List<Dropdown.OptionData>();
            femaleNames = new List<Dropdown.OptionData>();
            setNamesInDropdown();
        }

        private void setNamesInDropdown()
        {
            maleNames.Add(new Dropdown.OptionData("Eric"));
            maleNames.Add(new Dropdown.OptionData("Patrick"));
            maleNames.Add(new Dropdown.OptionData("Joe"));
            femaleNames.Add(new Dropdown.OptionData("Emma"));
            femaleNames.Add(new Dropdown.OptionData("Lousie"));
            femaleNames.Add(new Dropdown.OptionData("Sarah"));
        }

        public void setGender()
        {
            menuInteraction.newGame.gender = canvas.transform.GetChild(2).gameObject.GetComponent<Dropdown>().value;
            canvas.transform.GetChild(3).gameObject.GetComponent<Dropdown>().value = 0;
            setName();

            //canvas.transform.GetChild(3).gameObject.GetComponent<Dropdown>().ClearOptions();
            if (menuInteraction.newGame.gender == 0)
            {
                canvas.transform.GetChild(3).gameObject.GetComponent<Dropdown>().options = maleNames;
            }
            else
            {
                canvas.transform.GetChild(3).gameObject.GetComponent<Dropdown>().options = femaleNames;
            }

            //updateXml();
        }

        private void setInput()
        {
            menuInteraction.newGame.inputType = canvas.transform.GetChild(1).gameObject.GetComponent<Dropdown>().value;
            //updateXml();
        }

        public void setName()
        {
            int nameIndex = canvas.transform.GetChild(3).gameObject.GetComponent<Dropdown>().value;
            if (menuInteraction.newGame.gender == 0)
            {
                menuInteraction.newGame.name = maleNames[nameIndex].text;
            }
            else
            {
                menuInteraction.newGame.name = femaleNames[nameIndex].text;
            }

            //updateXml();
        }

        public void updateXml()
        {
            //setGender();
            setInput();
            //setName();
            string path = Path.Combine(Application.streamingAssetsPath, "Menu.xml");
            XMLWorker.serialize(menuInteraction, path);
            updateConfig();
        }

        private void updateConfig()
        {
            AimlSettings aimlSettings = XMLWorker.deserialize<AimlSettings>(pathToConfig);
            aimlSettings.settings.Find(x => x.nameOfAttribute == "name").valueOfAttribute =
                menuInteraction.newGame.name;
            aimlSettings.settings.Find(x => x.nameOfAttribute == "gender").valueOfAttribute =
                menuInteraction.newGame.gender == 0 ? "Male" : "Female";
            XMLWorker.serialize(aimlSettings, pathToConfig);
        }
    }
}
