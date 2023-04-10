using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UpperSlice : MonoBehaviour
{

    void Start()
    {
        transform.localScale = new Vector3(2f, 2f, 2f);
        Invoke("ToDestroy", 2f);
    }

    private void ToDestroy()
    {
        Destroy(gameObject);
    }
}
