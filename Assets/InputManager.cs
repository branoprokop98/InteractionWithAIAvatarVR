using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class InputManager : MonoBehaviour
{
    [SerializeField] private XRNode xrNode = XRNode.LeftHand;
    private InputDevice controller;
    private List<InputDevice> devices = new List<InputDevice>();
    private InputDevice device;

    private bool _leftTriggerDown;
    private bool _leftGripDown;
// and other left hand buttons

    private bool _rightTriggerDown;
    private bool _rightGripDown;

    // Start is called before the first frame update
    void Start()
    {
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller &
                                                   InputDeviceCharacteristics.TrackedDevice, devices);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (InputDevice inputDevice in devices)
        {
            if (inputDevice.characteristics.HasFlag(InputDeviceCharacteristics.Left))
            {
                // Left hand, grip button
                ProcessInputDeviceButton(inputDevice, InputHelpers.Button.PrimaryButton, ref _leftTriggerDown,
                    () => // On Button Down
                    {
                        Debug.Log("Left hand PrimaryButton down");
                        // Your functionality
                    },
                    () => // On Button Up
                    {
                        Debug.Log("Left hand PrimaryButton up");
                    });
                // Repeat ProcessInputDeviceButton for other buttons
            }
            // Repeat for right hand
        }
        // if (!device.isValid)
        // {
        //     getDevice();
        // }
        //
        //
        // List<InputFeatureUsage> features = new List<InputFeatureUsage>();
        // device.TryGetFeatureUsages(features);
        //
        // foreach (InputFeatureUsage feature in features)
        // {
        //     Debug.Log(feature.name + " " + feature.type);
        // }
        // bool triggerButtonAction;
        // if (device.TryGetFeatureValue(CommonUsages.primaryButton, out triggerButtonAction) && triggerButtonAction)
        // {
        //     Debug.Log($"Primary button: {triggerButtonAction}");
        // }
    }

    private void ProcessInputDeviceButton(InputDevice inputDevice, InputHelpers.Button button, ref bool _wasPressedDownPreviousFrame, Action onButtonDown = null, Action onButtonUp = null, Action onButtonHeld = null)
    {
        if (inputDevice.IsPressed(button, out bool isPressed) && isPressed)
        {
            if (!_wasPressedDownPreviousFrame) // // this is button down
            {
                onButtonDown?.Invoke();
            }

            _wasPressedDownPreviousFrame = true;
            onButtonHeld?.Invoke();
        }
        else
        {
            if (_wasPressedDownPreviousFrame) // this is button up
            {
                onButtonUp?.Invoke();
            }

            _wasPressedDownPreviousFrame = false;
        }
    }

    private void OnEnable()
    {
        if (!device.isValid)
        {
            getDevice();
        }
    }

    public void getDevice()
    {
        InputDevices.GetDevicesAtXRNode(xrNode, devices);
        device = devices.FirstOrDefault();
    }
}
