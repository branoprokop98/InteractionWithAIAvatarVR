using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class SpeechInputForAiml
{
    private DictationRecognizer _dictationRecognizer;

    public SpeechInputForAiml()
    {
        _dictationRecognizer = new DictationRecognizer();
    }

    public void speechInput(InputField inputField)
    {
        _dictationRecognizer.Start();
        _dictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogWarningFormat("Dictation result: {0}", text);
            //m_Recognitions += text + "\n";
            inputField.text = text;
        };
    }
}
