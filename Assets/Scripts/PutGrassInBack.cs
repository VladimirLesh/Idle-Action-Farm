using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PutGrassInBack : MonoBehaviour
{
    private bool isInCar = false;

    private void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOJump(transform.position + Vector3.up, 3f, 1, 1f));
        GetGrass.instance._grassArray.Add(this);
    }

    private void Update()
    {
        if (!isInCar)
        {
            transform.position = Vector3.Lerp(gameObject.transform.position, Move.Instance._back.transform.position, 0.1f);

            if ((transform.position - Move.Instance._back.transform.position).magnitude < 0.5f)
            {
                transform.position = Move.Instance._back.transform.position;
            }
        }
    }

    public void IsInCar() => isInCar = true;
}
