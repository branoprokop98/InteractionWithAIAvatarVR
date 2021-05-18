using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AIML;
using Menu;
using Menu.NewGame;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadGameFromSave : MonoBehaviour
{
    private string saveGameLocation;
    [SerializeField] private Image image;
    private List<Button> buttons;

    private string destinationFile;

    //private string sourceFile;
    private string backupFile;
    private string pathToConfig;

    private List<string> saves;

    // Start is called before the first frame update
    void Start()
    {
        buttons = new List<Button>();
        saves = new List<string>();
        saveGameLocation = Path.Combine(Application.streamingAssetsPath, "Save\\");
        destinationFile = Path.Combine(Application.streamingAssetsPath, "Menu.xml");
        backupFile = Path.Combine(Application.streamingAssetsPath, "Menu.xml.bac");
        pathToConfig = Path.Combine(Environment.CurrentDirectory, Path.Combine("config", "Settings.xml"));
        loadSaveButtons();
        findSaves();
    }

    private void findSaves()
    {
        string[] fileEntries = Directory.GetFiles(saveGameLocation, "*.xml");
        for (int i = 0; i < fileEntries.Length; i++)
        {
            string sourceFile = fileEntries[i];
            buttons[i].interactable = true;
            MenuInteraction menuInteraction = XMLWorker.deserialize<MenuInteraction>(sourceFile);
            buttons[i].GetComponentInChildren<Text>().text = menuInteraction.saveInfo.DateTime + " " +
                                                             menuInteraction.newGame.name + " " +
                                                             getGenderType(menuInteraction) + " " +
                                                             getInputType(menuInteraction);
            var i1 = i;
            buttons[i].onClick.AddListener(() => replaceMenuForSave(sourceFile, i1));
            buttons[i].onClick.AddListener(()=>selectButton(i1));
        }

        foreach (string fileName in fileEntries)
        {
            saves.Add(fileName);
        }
    }

    private string getGenderType(MenuInteraction menuInteraction)
    {
        switch (menuInteraction.newGame.gender)
        {
            case 0:
                return "\nGender: Male";
            case 1:
                return "\nGender: Female";
        }
        return null;
    }

    private string getInputType(MenuInteraction menuInteraction)
    {
        switch (menuInteraction.newGame.inputType)
        {
            case 0:
                return "\nInput: Keyboard input";
                break;
            case 1:
                return "\nInput: Speech input";
            case 2:
                return "\nInput: Context window";
        }

        return null;
    }



    private void replaceMenuForSave(string sourceFileName, int i)
    {
        File.Copy(sourceFileName, destinationFile, true);
        updateConfig();
        Debug.LogWarning("Buttin number: " + i);
        //File.Replace(sourceFile, destinationFile, backupFile);
    }

    private void updateConfig()
    {
        MenuInteraction menuInteraction = XMLWorker.deserialize<MenuInteraction>(destinationFile);
        AimlSettings aimlSettings = XMLWorker.deserialize<AimlSettings>(pathToConfig);
        aimlSettings.settings.Find(x => x.nameOfAttribute == "name").valueOfAttribute =
            menuInteraction.newGame.name;
        aimlSettings.settings.Find(x => x.nameOfAttribute == "gender").valueOfAttribute =
            menuInteraction.newGame.gender == 0 ? "Male" : "Female";
        XMLWorker.serialize(aimlSettings, pathToConfig);
    }

    private void loadSaveButtons()
    {
        for (int i = 0; i < image.transform.childCount; i++)
        {
            Button btn = image.transform.GetChild(i).gameObject.GetComponent<Button>();
            buttons.Add(btn);
        }
    }

    public void selectButton(Button button)
    {
        deselectAllButtons();
        button.GetComponent<Image>().color = new Color(126f / 255f, 159f / 255f, 105f / 255f);
    }
    public void selectButton(int index)
    {
        deselectAllButtons();
        buttons[index].GetComponent<Image>().color = new Color(126f / 255f, 159f / 255f, 105f / 255f);
    }

    public void deselectAllButtons()
    {
        for (int i = 0; i < image.transform.childCount; i++)
        {
            buttons[i].GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
        }
    }
}
