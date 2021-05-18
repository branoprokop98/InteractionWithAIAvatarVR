using System;
//using System.Speech.Synthesis;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Menu
{
    public class CheckCompatibility : MonoBehaviour
    {
        //private SpeechSynthesizer speechSynthesizer;
        private DictationRecognizer dictationRecognizer;
        private void Awake()
        {
            //speechSynthesizer = new SpeechSynthesizer();
        }

        // Start is called before the first frame update
        void Start()
        {
            checkVoices();
            checkSpeechInput();
        }

        private void checkVoices()
        {
            // foreach (var voice in speechSynthesizer.GetInstalledVoices())
            // {
            //     var info = voice.VoiceInfo;
            //     Debug.LogWarningFormat("Id: {0} | Name: {1} | Age: {2} | Gender: {3} | Culture: {4}", info.Id, info.Name, info.Age, info.Gender, info.Culture);
            // }
        }

        private void checkSpeechInput()
        {
            try
            {
                dictationRecognizer = new DictationRecognizer();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
