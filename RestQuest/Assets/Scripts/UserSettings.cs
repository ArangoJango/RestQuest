using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserSettings : MonoBehaviour
{
    public int BreakInterval;
    // Start is called before the first frame update
    void Start()
    {
        if (BreakInterval == 0)
        {
            BreakInterval = 30;
        }
    }

    public void SetBreakInterval ()
    {
        // User Setting here
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
