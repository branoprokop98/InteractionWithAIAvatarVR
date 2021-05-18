using System.Collections;
using System.Globalization;
using AIMLbot;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

namespace AIML.KeyboardInput
{
    [RequireComponent(typeof(Animator))]
    public class AI_Bot : MonoBehaviour
    {
        [SerializeField] private Text outText;
        [SerializeField] private Canvas canvas;
        [SerializeField] private InputField textField;
        [SerializeField] private GameObject botObject;

        [SerializeField] private Text moodText;
        //[SerializeField] private GameObject player;
        [SerializeField] private Text errorText;
        private Rigidbody _rigidbody;

        //private SpeechInputForAiml _speechInputForAiml;

        private Hiting _hiting;
        private RigidbodyFirstPersonController _rigidbodyFirstPersonController;
        private Aiml aiml;
        private Bot AI;
        private User myuser;
        private bool inDialog;
        private string text;
        private float startTime;
        private Animator animator;
        private bool toChange;

        public static AI_Bot aiBot;

        //private SpeechInput _speechInput;

        public string Text
        {
            get => text;
            set => text = value;
        }

        private void Awake()
        {
            //_speechInput = new SpeechInput();
            AI = new Bot();
            myuser = new User("Username Here", AI);
            AI.loadSettings(); //It will Load Settings from its Config Folder with this code
            AI.loadAIMLFromFiles(); //With this Code It Will Load AIML Files from its AIML Folder
        }

        void Start()
        {
            toChange = false;
            //_speechInputForAiml = new SpeechInputForAiml();
            animator = this.GetComponent<Animator>();
            aiBot = this;
            aiml = new Aiml(animator);
            aiml.time = 179f;
            outText = outText.GetComponent<Text>();
            _rigidbody = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Rigidbody>();
            //_rigidbody = player.GetComponent<Rigidbody>();
            canvas.enabled = false;
            _hiting = new Hiting(60);
            inDialog = false;
            errorText = errorText.GetComponent<Text>();
            errorText.enabled = true;
            ShowCursor.mouseInvisible();
            StartCoroutine(startTimer());
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && _hiting.getHit() && botObject == _hiting.hit.collider.gameObject &&
                inDialog == false)
            {
                ShowCursor.mouseVisible();
                _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                canvas.enabled = true;
                RigidbodyFirstPersonController.instance.mouseLook.XSensitivity = 0;
                RigidbodyFirstPersonController.instance.mouseLook.YSensitivity = 0;
                inDialog = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && inDialog)
            {
                ShowCursor.mouseInvisible();
                _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                canvas.enabled = false;
                RigidbodyFirstPersonController.instance.mouseLook.XSensitivity = 2;
                RigidbodyFirstPersonController.instance.mouseLook.YSensitivity = 2;
                inDialog = false;
            }

            if (toChange)
            {
                changeMood120();
            }
        }


        public void botControll(string text)
        {
            aiml.botInput(text, outText, errorText, moodText);

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

                this.errorText.text = aiml.time.ToString(CultureInfo.InvariantCulture) + " " + Aiml.mood;
                yield return new WaitForSeconds(1f);
            }
        }

    }
}
