using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Menu;
using Menu.NewGame;
using Menu.SettingsGame;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using Debug = UnityEngine.Debug;


public class SpeechCompatibility : MonoBehaviour
{
    private DictationRecognizer dictationRecognizer;
    [SerializeField] private Canvas speechCanvas;
    [SerializeField] private Canvas textToSpeechCanvas;
    private Process process;
    private List<string> output;
    private TaskCompletionSource<bool> eventHandled;
    private Settings settings;
    private string pathToSettings;
    public static bool textToSpeechCompatibility;

    // Start is called before the first frame update
    private void Awake()
    {
        pathToSettings = Path.Combine(Application.streamingAssetsPath, "Settings.xml");
        settings = XMLWorker.deserialize<Settings>(pathToSettings);
        speechCanvas.enabled = false;
        textToSpeechCanvas.enabled = false;
        try
        {
            dictationRecognizer = new DictationRecognizer();
            output = new List<string>();
            dictationRecognizer.Start();
            dictationRecognizer.DictationResult += (text, confidence) =>
            {
                Debug.LogFormat("{0} {1}",text, confidence);
            };
            dictationRecognizer.Stop();
            dictationRecognizer.Dispose();
            textToSpeechCompatibility = true;
            runProcess();
        }
        catch (Exception e)
        {
            speechCanvas.enabled = true;
            textToSpeechCompatibility = false;
            ShowCursor.mouseVisible();
        }
    }

    private void Start()
    {
        if (output.Count == 2)
        {
            textToSpeechCanvas.enabled = true;
            //XMLWorker.serialize(settings, Path.Combine(Application.streamingAssetsPath, "Menu.xml"));
            textToSpeechCanvas.GetComponentInChildren<Text>().text = "Your system supports only " + output[0] + " voice";
        }

        if (output.Count == 1)
        {
            textToSpeechCanvas.enabled = true;
            //XMLWorker.serialize(settings, Path.Combine(Application.streamingAssetsPath, "Menu.xml"));
            textToSpeechCanvas.GetComponentInChildren<Text>().text = "Your system does not have installed voices for text to speech";
        }

        if (output.Count > 2)
        {
            textToSpeechCanvas.enabled = false;
            //XMLWorker.serialize(settings, Path.Combine(Application.streamingAssetsPath, "Menu.xml"));
        }
    }

    public void hideSpeechCanvas()
    {
        speechCanvas.enabled = false;
        ShowCursor.mouseVisible();;
    }
    public void hideTextToSpeechCanvas()
    {
        textToSpeechCanvas.enabled = false;
        ShowCursor.mouseVisible();
    }

    public void runProcess()
    {
        try
        {
            process = new Process();
            eventHandled = new TaskCompletionSource<bool>();
            process.StartInfo.FileName = Path.Combine(Application.dataPath + @"\StreamingAssets" + @"\TextToSpeechGetVoices.exe");
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.EnableRaisingEvents = true;
            process.OutputDataReceived += new DataReceivedEventHandler( DataReceived );
            process.ErrorDataReceived += new DataReceivedEventHandler( ErrorReceived );
            //process.Exited += new EventHandler(checkErrorTextToSpeech);
            process.Start();
            process.BeginOutputReadLine();
            StreamWriter  messageStream = process.StandardInput;

            UnityEngine.Debug.Log( "Successfully launched app" );
        }
        catch( Exception e )
        {
            UnityEngine.Debug.LogError( "Unable to launch app: " + e.Message );
        }
    }

    void DataReceived( object sender, DataReceivedEventArgs eventArgs )
    {
        output.Add(eventArgs.Data);
        if (eventArgs.Data == null)
        {
            if (output.Count == 2)
            {
                settings.speechOutputSupport = true;
            }

            if (output.Count == 1)
            {
                settings.speechOutputSupport = false;
            }

            if (output.Count > 2)
            {
                settings.speechOutputSupport = true;
            }
            XMLWorker.serialize(settings, Path.Combine(Application.streamingAssetsPath, "Settings.xml"));
            //UnityMainThreadDispatcher.Instance().Enqueue(setSpeechCompatibility);
            eventHandled.TrySetResult(true);
        }
    }

    private void setSpeechCompatibility()
    {
        if (output.Count == 2)
        {
            textToSpeechCanvas.enabled = true;
            //XMLWorker.serialize(settings, Path.Combine(Application.streamingAssetsPath, "Menu.xml"));
            textToSpeechCanvas.GetComponentInChildren<Text>().text = "Your system supports only " + output[0] + " voice";
        }

        if (output.Count == 1)
        {
            textToSpeechCanvas.enabled = true;
            //XMLWorker.serialize(settings, Path.Combine(Application.streamingAssetsPath, "Menu.xml"));
            textToSpeechCanvas.GetComponentInChildren<Text>().text = "Your system does not have installed voices for text to speech";
        }

        if (output.Count > 2)
        {
            textToSpeechCanvas.enabled = false;
            //XMLWorker.serialize(settings, Path.Combine(Application.streamingAssetsPath, "Menu.xml"));
        }
    }


    void ErrorReceived( object sender, DataReceivedEventArgs eventArgs )
    {
        UnityEngine.Debug.LogError( eventArgs.Data );
    }

    public void checkErrorTextToSpeech()
    {
        if (output.Count == 2)
        {
            settings.speechOutputSupport = true;
        }

        if (output.Count == 1)
        {
            settings.speechOutputSupport = false;
        }

        if (output.Count > 2)
        {
            settings.speechOutputSupport = true;
        }
        saveSettings();
    }

    public void saveSettings()
    {
        if (!settings.speechOutputSupport)
        {
            settings.volume = 0;
        }
        XMLWorker.serialize(settings, Path.Combine(Application.streamingAssetsPath, "Settings.xml"));
    }
}
