using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extract : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private Vector3 _sizeBoxCollider = new Vector3(0.26f, 0.14f, 0.42f);
    private Vector3 _center = Vector3.zero;
    private Rigidbody _rigidbody;
    private MeshCollider _meshCollider;

    private void Start()
    {
        _boxCollider = gameObject.AddComponent<BoxCollider>();
        _boxCollider.size = _sizeBoxCollider;
        _boxCollider.center = _center;
        _rigidbody = GetComponent<Rigidbody>();
        _meshCollider = GetComponent<MeshCollider>();
        _rigidbody.useGravity = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (GetGrass.instance._sliderBack.value != GetGrass.instance._sliderBack.maxValue)
            {
                isFalse();
                _meshCollider.enabled = false;
                transform.parent = Move.Instance._back.transform;
                PutGrassInBack putGrass = gameObject.AddComponent<PutGrassInBack>();
                _rigidbody.isKinematic = true;
                _rigidbody.useGravity = false;
                gameObject.layer = 6;
                GetGrass.instance._sliderBack.value += 1f;
                GetGrass.instance.text.text = $"{GetGrass.instance._sliderBack.value}/{GetGrass.instance._sliderBack.maxValue}";

                foreach (Transform child in transform)
                {
                    child.gameObject.layer = 6;
                    foreach (Transform childs in child.transform)
                    {
                        childs.gameObject.layer = 6;
                    }
                }
            }
        }
    }

    public void isFalse()
    {
        _boxCollider.enabled = false;
        _rigidbody.useGravity = false;
    }
}
