using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTest : MonoBehaviour
{

    public static bool onSelect = false;

    public void onHoverTest()
    {
        Debug.Log("Hovered");
    }

    public void onSelectObject()
    {
        if (onSelect)
        {
            onSelect = false;
            Debug.Log("Deselect");
        }
        else
        {
            onSelect = true;
            Debug.Log("Select");
        }
    }
    public void onSelectExitObject()
    {
        Debug.Log("Selected Exited");
    }
}
