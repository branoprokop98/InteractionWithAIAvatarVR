
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Canvas))]
public class AssignCamera : MonoBehaviour
{

    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<Camera>();
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            canvas.planeDistance = 1;
        }
    }

}
