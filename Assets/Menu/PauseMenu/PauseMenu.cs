using System;
using System.Collections.Generic;
using System.Linq;
using Menu.SaveLoadGame;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

//using UnityEngine.XR.Interaction.Toolkit;

namespace Menu.PauseMenu
{
    public class PauseMenu : MonoBehaviour
    {
        public Canvas pauseMenuCanvas;
        [SerializeField] private XRNode xRNode = XRNode.LeftHand;

        private List<InputDevice> devices = new List<InputDevice>();

        private InputDevice device;

        private bool isPaused;
        private SaveGameController save;

        //to avoid repeat readings
        private bool primaryButtonIsPressed;

        void GetDevice()
        {
            InputDevices.GetDevicesAtXRNode(xRNode, devices);
            device = devices.FirstOrDefault();
        }

        void OnEnable()
        {
            if (!device.isValid)
            {
                GetDevice();
            }
        }

        private void Start()
        {
            save = new SaveGameController();
            pauseMenuCanvas.enabled = false;
            isPaused = false;
        }

        void Update()
        {
            if (!device.isValid)
            {
                GetDevice();
            }

            // capturing primary button press and release
            bool primaryButtonValue = false;
            InputFeatureUsage<bool> primaryButtonUsage = CommonUsages.primaryButton;

            if (device.TryGetFeatureValue(primaryButtonUsage, out primaryButtonValue) && primaryButtonValue &&
                !primaryButtonIsPressed)
            {
                primaryButtonIsPressed = true;
                Debug.LogFormat($"PrimaryButton activated {primaryButtonValue} on {xRNode}");

                if (isPaused)
                {
                    resume();
                }
                else
                {
                    pause();
                }

            }
            else if (!primaryButtonValue && primaryButtonIsPressed)
            {
                primaryButtonIsPressed = false;
                Debug.LogFormat($"PrimaryButton deactivated {primaryButtonValue} on {xRNode}"); }
        }

        public void resume()
        {
            //pauseCanvas.SetActive(false);
            pauseMenuCanvas.enabled = false;
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
                pauseMenuCanvas.enabled = true;
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

// [SerializeField] private XRNode xrNode = XRNode.LeftHand;
//
// private List<InputDevice> devices = new List<InputDevice>();
//
// private InputDevice device;
//
// void OnEnable()
// {
//     if (!device.isValid)
//     {
//         getDevice();
//     }
// }
//
// public void getDevice()
// {
//     InputDevices.GetDevicesAtXRNode(xrNode, devices);
//     device = devices.FirstOrDefault();
// }
//
// private void Start()
// {
//     pauseMenuCanvas.enabled = false;
// }
//
// private void Update()
// {
//     if (!device.isValid)
//     {
//         getDevice();
//     }
//
//     List<InputFeatureUsage> features = new List<InputFeatureUsage>();
//     device.TryGetFeatureUsages(features);
//
//     foreach (InputFeatureUsage feature in features)
//     {
//         Debug.LogWarning("Feature: " +  feature.name + " type: " + feature.type);
//     }
// }
