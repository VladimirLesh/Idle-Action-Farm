using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Barn : GetSourse
{
    IEnumerator MoveToBarn()
    {
        for (int i = GetGrass.instance._grassArray.Count - 1; i > -1; i--)
        {
            yield return new WaitForSeconds(0.05f);
            if (i >= GetGrass.instance._grassArray.Count)
            {
                yield break;
            }
            GetGrass.instance._sliderBack.value -= 1f;
            GetGrass.instance.text.text = $"{GetGrass.instance._sliderBack.value}/{GetGrass.instance._sliderBack.maxValue}";
            GetGrass.instance._grassArray[i].transform.parent = transform;
            GetGrass.instance._grassArray[i].IsInCar();
            Rigidbody rb = GetGrass.instance._grassArray[i].GetComponent<Rigidbody>();
            GetGrass.instance._grassArray[i].transform.DOJump(_point.transform.position, 6f, 1, 1f).OnComplete(() => Destroy(rb.gameObject));
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.freezeRotation = false;
            MeshCollider bc = GetGrass.instance._grassArray[i].GetComponent<MeshCollider>();
            bc.isTrigger = false;
            GetGrass.instance._grassArray.Remove(GetGrass.instance._grassArray[i]);
            _uiController.MoveCoin();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (GetGrass.instance._grassArray != null)
            {
                StartCoroutine(MoveToBarn());
            }
        }
    }
}
