using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using DG.Tweening;
using Vector3 = UnityEngine.Vector3;

public class GetObject : MonoBehaviour
{
    public Transform objectHolder;
    public Vector3 targetVector3;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Cube cube))
        {
            CollectObject(cube);
        }
    }

    

    private void CollectObject(Cube cube)
    {
        targetVector3 = objectHolder.transform.position + Vector3.up; 
        Sequence sequence = DOTween.Sequence();
        sequence.Append(cube.transform.DOJump(targetVector3, 7f, 1, 0.4f))
            .OnComplete(()=> cube.gameObject.SetActive(false));
    }
}