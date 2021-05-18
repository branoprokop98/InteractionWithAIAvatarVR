using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{

    public static float time;

    public static bool counting;
    // Start is called before the first frame update

    public Timer()
    {
        time = 180f;
        counting = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.LogWarning("Time Left:" + Mathf.Round(time));
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     if (counting == false)
        //     {
        //         counting = true;
        //     }else if (counting)
        //     {
        //         counting = false;
        //     }
        // }
        //
        // if (counting)
        // {
        //     updateTime();
        // }
        //
        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     restoreTime();
        // }
    }

    public void updateTime()
    {
        Debug.LogWarning("Time Left:" + Mathf.Round(time));
        time -= Time.deltaTime;
    }

    public void restoreTime()
    {
        time = 180f;
    }
}
