using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

namespace AIML.SpeechInput
{
    [RequireComponent(typeof(Animator))]
    public class SpeechInput : MonoBehaviour
    {
        [SerializeField] private GameObject interactObject;
        [SerializeField] private Canvas interactCanvas;
        [SerializeField] private Text speechText;
        [SerializeField] private Text outText;
        [SerializeField] private Text errorText;
        [SerializeField] private Text moodText;

        private DictationRecognizer dictationRecognizer;
        private string m_Recognitions;
        private Hiting hitting;
        public static bool interacting;
        private Aiml aiml;
        private string recognizedString;
        private Animator animator;
        private Timer timer;
        private bool toChange;


        private void Start()
        {
            toChange = false;
            interacting = false;
            hitting = new Hiting(60);
            interactCanvas.enabled = false;
            errorText = errorText.GetComponent<Text>();
            animator = this.GetComponent<Animator>();
            aiml = new Aiml(animator);
            aiml.time = 179f;
            errorText.enabled = true;
            timer = new Timer();
            Timer.counting = true;
            StartCoroutine(startTimer());
        }

        private void Update()
        {
            if (HoverTest.onSelect &&
                interacting == false)
            {
                interacting = true;
                interactCanvas.enabled = true;
                dictationRecognizer = new DictationRecognizer();
                dictationRecognizer.Start();
                speechInput();
            }
            else if (!HoverTest.onSelect && interacting)
            {
                Timer.counting = false;
                interacting = false;
                interactCanvas.enabled = false;
                dictationRecognizer.Stop();
                // StartCoroutine(startTimer());
                //time = 181f;
                dictationRecognizer.Dispose();
            }

            if (toChange)
            {
                changeMood120();
            }
        }

        private void changeMood120()
        {
            if (Aiml.mood > 70)
            {
                aiml.setMoodAnimation();
            }
            else if (Aiml.mood <= 70 && Aiml.mood > 30)
            {
                aiml.setMoodAnimation();
            }
            else if (Aiml.mood <= 30)
            {
                aiml.setMoodAnimation();
            }

            toChange = false;
        }

        private void speechInput()
        {
            dictationRecognizer.DictationResult += (text, confidence) =>
            {
                Debug.LogWarningFormat("Dictation result: {0} , {1}", text, confidence);
                //m_Recognitions += text + "\n";
                speechText.text = text;
                aiml.botInput(text, outText, errorText, moodText);
            };
        }

        public IEnumerator startTimer()
        {
            while (true)
            {
                aiml.time--;
                if (aiml.time % 60 == 0 && aiml.time != 0)
                {
                    if (Aiml.mood > 70)
                    {
                        Aiml.mood = 60;
                    }
                    else if (Aiml.mood <= 70 && Aiml.mood > 30)
                    {
                        Aiml.mood = 20;
                    }
                    else if (Aiml.mood <= 30)
                    {
                        Aiml.mood = 0;
                    }
                    toChange = true;
                }

                if (aiml.time <= 0)
                {
                    aiml.time++;
                }

                //this.errorText.text = aiml.time.ToString(CultureInfo.InvariantCulture) + " " + Aiml.mood;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
