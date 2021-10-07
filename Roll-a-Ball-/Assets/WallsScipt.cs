using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsScipt : MonoBehaviour
{
    public bool isHot;

    private void Start()
    {
        isHot = false;
    }

    private void Update()
    {
        if (isHot == true)
        {
            print("yeeeeeee");
        }
    }

}
