using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using InputDevice = UnityEngine.XR.InputDevice;

public class TestInput : MonoBehaviour
{
    // Start is called before the first frame update
    public InputActionReference menuAction { get; set; }
    private InputDevice inputDevice;
    private ActionBasedController actionBasedController;
    void Start()
    {
        //actionBasedController = GetComponent<ActionBasedController>();
        //bool isPressed = actionBasedController.selectAction.action.ReadValue<bool>();
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
        //InputHelpers.Button button = actionBasedController.selectAction.action.ReadValue<InputHelpers.Button>();
        //Debug.LogWarning(button);
        bool triggerValue;
        if (inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton,
                out triggerValue)
            && triggerValue)
        {
            Debug.Log("Trigger button is pressed");
        }
        Debug.Log(triggerValue);
    }
}
