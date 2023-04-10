using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private void Update()
    {
        if (!gameObject.activeSelf)
        {
            Destroy(this.gameObject);
        }
    }
}
