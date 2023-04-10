using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player PlayerInstance;

    private void Start()
    {
        if (PlayerInstance != null)
        {
            Destroy(this);
            return;
        }
        PlayerInstance = this;
        
        
    }
}
