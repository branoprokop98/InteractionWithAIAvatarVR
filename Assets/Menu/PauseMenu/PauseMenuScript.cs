using System.Collections.Generic;
using Menu.SaveLoadGame;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

namespace Menu.PauseMenu
{
    public class PauseMenuScript : MonoBehaviour
    {
        [SerializeField] private Canvas pauseCanvas;
        //[SerializeField] private Canvas hitCanvas;
        private SaveGameController save;

        private InputDevice inputDevice;

        public XRController left;
        public XRController right;
        public InputHelpers.Button  activateButton;
        public float activationThreshold = 0.1f;


        private bool isPaused;
        // Start is called before the first frame update
        void Start()
        {
            isPaused = false;
            //pauseCanvas.SetActive(false);
            pauseCanvas.enabled = false;
            //hitCanvas.enabled = true;
            save = new SaveGameController();
            ShowCursor.mouseInvisible();

            List<InputDevice> devices = new List<InputDevice>();
            InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
            InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);

            foreach (InputDevice item in devices)
            {
                Debug.Log(item.name + item.characteristics);
            }

            var inputDevices = new List<UnityEngine.XR.InputDevice>();
            UnityEngine.XR.InputDevices.GetDevices(inputDevices);

            foreach (var device in inputDevices)
            {
                Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
                inputDevice = device;
            }
        }

        // Update is called once per frame
        void Update()
        {
            bool triggerValue;
            var device = XRNode.RightHand;
            if (inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton,
                    out triggerValue)
                && triggerValue)
            {
                if (isPaused)
                {
                    resume();
                }
                else
                {
                    pause();
                }
            }

            // if (Input.GetKeyDown(KeyCode.Escape))
            // {
            //     if (isPaused)
            //     {
            //         resume();
            //     }
            //     else
            //     {
            //         pause();
            //     }
            // }
        }

        // public bool checkIfActivated(XRController controller)
        // {
        //     InputHelpers.IsPressed(controller., activateButton, out bool isActivated, activationThreshold);
        // }

        public void resume()
        {
            //pauseCanvas.SetActive(false);
            pauseCanvas.enabled = false;
            //hitCanvas.enabled = true;
            ShowCursor.mouseInvisible();
            Time.timeScale = 1f;
            isPaused = false;
            Debug.Log("Resume game ...");
        }

        private void pause()
        {
            //pauseCanvas.SetActive(true);
            if (!InteractCanvas.interacting && InteractCanvas.showPauseMenu)
            {
                pauseCanvas.enabled = true;
                //hitCanvas.enabled = false;
                ShowCursor.mouseVisible();
                Time.timeScale = 0.1f;
                isPaused = true;
            }
        }

        public void saveGame()
        {
            save.saveGame();
        }

        public void loadMenu()
        {
            SceneManager.LoadScene("Menu");
            Time.timeScale = 1f;
            Debug.Log("Load Menu ...");
        }
    }
}
