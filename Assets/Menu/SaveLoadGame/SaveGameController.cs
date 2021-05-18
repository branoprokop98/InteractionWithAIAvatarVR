using System;
using System.IO;
using AIML;
using Menu.NewGame;
using UnityEngine;

namespace Menu.SaveLoadGame
{
    public class SaveGameController
    {
        private string fileName;
        private string fileToSave;
        private string savePath;
        private int counter;
        private MenuInteraction menuInteraction;

        public SaveGameController()
        {
            counter = 0;
            fileName = "Save" + counter;
            this.fileToSave = Path.Combine(Application.streamingAssetsPath, "Menu.xml");
            this.savePath = Path.Combine(Application.streamingAssetsPath, "Save\\" + fileName + ".xml");
            menuInteraction = XMLWorker.deserialize<MenuInteraction>(fileToSave);
        }


        public void saveGame()
        {
            bool saved = false;
            menuInteraction.saveInfo.DateTime = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            foreach (DialogueHistory dialogueHistory in Aiml.dialogueHistories)
            {
                menuInteraction.dialogueHistories.Add(dialogueHistory);
            }
            menuInteraction.saveInfo.mood = Aiml.mood;
            XMLWorker.serialize(menuInteraction, fileToSave);
            if (File.Exists(savePath))
            {
                countFileName();
            }

            while (!saved)
            {
                try
                {
                    File.Copy(fileToSave, savePath);
                    saved = true;
                }
                catch
                {
                    countFileName();
                }
            }
        }

        private void countFileName()
        {
            counter++;
            fileName = "Save" + counter;
            this.savePath = Path.Combine(Application.streamingAssetsPath, "Save\\" + fileName + ".xml");
        }

    }
}
