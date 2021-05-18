using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Hiting
{
    private Ray ray;
    public RaycastHit hit;
    private int hitDistance;
    private LayerMask layerMask;

    public Hiting(int hitDistance)
    {
        this.hitDistance = hitDistance;
    }

    public Hiting()
    {
        hitDistance = 20;
    }

    public Hiting(LayerMask layerMask)
    {
        this.layerMask = layerMask;
    }

    public bool getHit()
    {
        if (!(Camera.main is null)) ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits =  Physics.RaycastAll(ray, 20, layerMask);
        if (Physics.Raycast(ray, out hit, hitDistance))
        {
            Debug.Log(hit.collider.gameObject);
            return true;
        }

        return false;
    }
}
