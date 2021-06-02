using System.Collections;
using System.Collections.Generic;
using System.IO;
using Menu;
using Menu.NewGame;
using UnityEngine;

public class LoadedManagment : MonoBehaviour
{
    [SerializeField] private GameObject speechInputMale;
    [SerializeField] private GameObject speechInputFemale;
    [SerializeField] private GameObject keyboardInputMale;
    [SerializeField] private GameObject keyboardInputFemale;
    [SerializeField] private GameObject contextWindowInputMale;
    [SerializeField] private GameObject contextWindowInputFemale;

    private MenuInteraction menuInteraction;
    // Start is called before the first frame update
    void Start()
    {
        ShowCursor.mouseInvisible();
        string path = Path.Combine(Application.streamingAssetsPath, "Menu.xml");
        menuInteraction = XMLWorker.deserialize<MenuInteraction>(path);
        loadInput();
    }

    private void loadInput()
    {
        switch (menuInteraction.newGame.inputType)
        {
            case 0:
                //context window
                loadGenderForContextWindow();
                break;
            case 1:
                //speech
                loadGenderForSpeech();
                break;
            case 2:
                //keyboard
                loadGenderForKeyboard();
                break;
        }
    }


    private void loadGenderForSpeech()
    {
        switch (menuInteraction.newGame.gender)
        {
            case 0:
                Instantiate(speechInputMale, speechInputMale.transform.position, speechInputMale.transform.rotation);
                break;
            case 1:
                Instantiate(speechInputFemale, speechInputFemale.transform.position, speechInputFemale.transform.rotation);
                break;
        }
    }

    private void loadGenderForKeyboard()
    {
        switch (menuInteraction.newGame.gender)
        {
            case 0:
                Instantiate(keyboardInputMale, keyboardInputMale.transform.position, keyboardInputMale.transform.rotation);
                break;
            case 1:
                Instantiate(keyboardInputFemale, keyboardInputFemale.transform.position, keyboardInputFemale.transform.rotation);
                break;
        }
    }

    private void loadGenderForContextWindow()
    {
        switch (menuInteraction.newGame.gender)
        {
            case 0:
                Instantiate(contextWindowInputMale, contextWindowInputMale.transform.position, contextWindowInputMale.transform.rotation);
                break;
            case 1:
                Instantiate(contextWindowInputFemale, contextWindowInputFemale.transform.position, contextWindowInputFemale.transform.rotation);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
