using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

namespace AIML.ContextWindowInput
{
    [RequireComponent(typeof(Animator))]
    public class ContextWindowService : MonoBehaviour
    {
        private ContextWindowTopic contextTopic;
        private ContextWindowSentence contextSentence;
        [SerializeField] private Canvas canvas;
        [SerializeField] private GameObject interactObject;
        [SerializeField] private Canvas textCanvas;
        [SerializeField] private Text errorText;

        private Hiting hitting;
        private bool interacting;
        public static int actualLayerOfTopic { get; set; }
        public static int actualLayerOfSentences { get; set; }
        private Aiml aiml;
        private Button btn;
        private Animator animator;
        private bool toChange;

        // Start is called before the first frame update
        void Start()
        {
            contextTopic = new ContextWindowTopic(canvas);
            animator = this.gameObject.GetComponent<Animator>();
            aiml = new Aiml(animator);
            contextSentence = new ContextWindowSentence(canvas, textCanvas, animator, aiml);
            canvas.enabled = false;
            hitting = new Hiting(60);
            interacting = false;
            aiml.time = 179f;
            actualLayerOfTopic = 0;
            actualLayerOfSentences = 0;
            textCanvas.enabled = false;
            ShowCursor.mouseInvisible();
            StartCoroutine(startTimer());
        }

        // Update is called once per frame
        void Update()
        {

            if (HoverTest.onSelect)
            {
                ShowCursor.mouseVisible();
                textCanvas.enabled = true;
                canvas.enabled = true;
                interacting = true;
            }
            else if (!HoverTest.onSelect)
            {
                ShowCursor.mouseInvisible();
                textCanvas.enabled = false;
                canvas.enabled = false;
                interacting = false;
            }

            if (toChange)
            {
                changeMood120();
            }
        }

        private void checkPressButton()
        {
        }

        public void getNextLayerOfTopic() => contextTopic.getNextLayer();


        public void getPrevLayerOfTopic() => contextTopic.getPrevLayer();

        public void getNextLayerOfSentence() => contextSentence.getNextLayer();

        public void getPrevLayerOfSentence() => contextSentence.getPrevLayer();

        public void getSentencesOfTopic(Button button) => contextSentence.getSentencesOfTopic(button);

        public void getTopics() => contextTopic.initTopicsName();

        public void OnHoverEnter(Button button)
        {
            button.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }

        public void OnHoverExit(Button button)
        {
            button.transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
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


// public void initTopicsName()
// {
//     actualLayerOfTopic = 0;
//     actualLayerOfSentences = -2;
//     for (int i = 0; i < canvas.transform.childCount - 3; i++)
//     {
//         Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
//         Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
//         try
//         {
//             buttonText.text = topics.ListOfTopics[actualLayerOfTopic][i].TopicName;
//             button.gameObject.SetActive(true);
//         }
//         catch (Exception e)
//         {
//             button.gameObject.SetActive(false);
//         }
//     }
// }
//
// public void getNextLayer()
// {
//     actualLayerOfTopic++;
//     if (tryLayerOfTopicBounce(1) == -1)
//     {
//         return;
//     };
//
//     for (int i = 0; i < canvas.transform.childCount - 3; i++)
//     {
//         Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
//         Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
//         try
//         {
//             buttonText.text = topics.ListOfTopics[actualLayerOfTopic][i].TopicName;
//             button.gameObject.SetActive(true);
//         }
//         catch (Exception e)
//         {
//             button.gameObject.SetActive(false);
//         }
//     }
// }
//
// public void getPrevLayer()
// {
//     actualLayerOfTopic--;
//     if (tryLayerOfTopicBounce(0) == -1)
//     {
//         return;
//     };
//
//     for (int i = 0; i < canvas.transform.childCount - 3; i++)
//     {
//         Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
//         Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
//         try
//         {
//             buttonText.text = this.topics.ListOfTopics[actualLayerOfTopic][i].TopicName;
//             button.gameObject.SetActive(true);
//         }
//         catch (Exception e)
//         {
//             button.gameObject.SetActive(false);
//         }
//     }
// }

// public void getSentencesOfTopic(Button button)
// {
//     Debug.Log(button.transform.GetChild(0).gameObject.GetComponent<Text>().text);
//     string nameOfTopic = button.transform.GetChild(0).gameObject.GetComponent<Text>().text;
//     foreach (List<Topics> listsOfTopics in topics.ListOfTopics)
//     {
//         foreach (Topics topic in listsOfTopics)
//         {
//             if (topic.TopicName.Equals(nameOfTopic))
//             {
//                 loadSentences.listSentences(topic.PathToTopic);
//                 initSentences();
//                 return;
//             }
//         }
//     }
//
//     Text outText = textCanvas.transform.GetChild(0).gameObject.GetComponent<Text>();
//     Text errorText = textCanvas.transform.GetChild(1).gameObject.GetComponent<Text>();
//     aiml.botInput(nameOfTopic, outText, errorText);
// }
//
// public void initSentences()
// {
//     actualLayerOfSentences = 0;
//     actualLayerOfTopic = -2;
//     for (int i = 0; i < canvas.transform.childCount - 3; i++)
//     {
//         Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
//         Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
//         try
//         {
//             buttonText.text = loadSentences.ListOfAimlSentences[actualLayerOfSentences][i].Pattern;
//             button.gameObject.SetActive(true);
//         }
//         catch (Exception e)
//         {
//             button.gameObject.SetActive(false);
//         }
//     }
// }
//
// public void getNextLayerSentence()
// {
//     actualLayerOfSentences++;
//     if (tryLayerOfSentencesBounce(1) == -1)
//     {
//         return;
//     }
//
//     for (int i = 0; i < canvas.transform.childCount - 3; i++)
//     {
//         Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
//         Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
//         try
//         {
//             buttonText.text = loadSentences.ListOfAimlSentences[actualLayerOfSentences][i].Pattern;
//             button.gameObject.SetActive(true);
//         }
//         catch (Exception e)
//         {
//             button.gameObject.SetActive(false);
//         }
//     }
// }
//
// public void getPrevLayerSentence()
// {
//     actualLayerOfSentences--;
//     if (tryLayerOfSentencesBounce(0) == -1)
//     {
//         return;
//     }
//
//     for (int i = 0; i < canvas.transform.childCount - 3; i++)
//     {
//         Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
//         Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
//         try
//         {
//             buttonText.text = loadSentences.ListOfAimlSentences[actualLayerOfSentences][i].Pattern;
//             button.gameObject.SetActive(true);
//         }
//         catch (Exception e)
//         {
//             button.gameObject.SetActive(false);
//         }
//     }
// }
