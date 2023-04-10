using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EzySlice;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Grass : MonoBehaviour
{
    public GameObject view;
    [SerializeField] private GameObject _box;
    [SerializeField] private GameObject _grass;
    public bool isReady;
    [SerializeField] private Material sliceMat;

    private void Start()
    {
        isReady = true;
    }

    // создаем новый объект для нарезки
    public void OnCreateHull(Vector3 vectorDirection)
    {
        Move.Instance.isChop = false;

        SlicedHull _hull = SliceTools.SliceObject(_grass, transform.position, vectorDirection, sliceMat);
        Slice(_hull);
    }

    private void Slice(SlicedHull hull)
    {
        SliceUper(hull);
        SliceLower();
    }

    private void SliceUper(SlicedHull hull)
    {
        if (hull != null)
        {
            GameObject uperHull = hull.CreateUpperHull(_grass, sliceMat);
            uperHull.transform.position = gameObject.transform.position;
            uperHull.transform.localScale = new Vector3(3f, 3f, 3f);
            Extract extract = uperHull.AddComponent<Extract>();
            SliceObject(uperHull, true);
        }
    }

    private void SliceLower()
    {
        GameObject lowerHull = CreateStumpSlice();
        lowerHull.transform.parent = gameObject.transform.parent;
        lowerHull.transform.localScale = new Vector3(3f, 3f, 3f);
        GrassParent GP = GetComponentInParent<GrassParent>();
        Stump stump = lowerHull.AddComponent<Stump>();
        GP.SetGrass(this);
        GP.StartCor();
        view.SetActive(false);
        isReady = false;
    }

    // создаем пенек
    private GameObject CreateStumpSlice()
    {
        SlicedHull _hull = SliceTools.SliceObject(_grass, transform.position, Vector3.down, sliceMat);
        GameObject uperHull = _hull.CreateUpperHull(_grass, sliceMat);
        SliceObject(uperHull, false);
        uperHull.transform.position = this.transform.position;
        return uperHull;
    }

    private void MoveSliceToBack(GameObject obj)
    {
        obj.transform.parent = Move.Instance._back.transform;
        PutGrassInBack putGrass = obj.AddComponent<PutGrassInBack>();
    }

    private void SliceObject(GameObject shatteredObject, bool jump)
    {
        if (shatteredObject == null)
            return;

        MeshCollider mcUper = SliceTools.FaultoleranceGetComponent<MeshCollider>(shatteredObject);
        if (mcUper != null)
        {
            mcUper.sharedMesh = shatteredObject.GetComponent<MeshFilter>().mesh;
            mcUper.convex = true;
            mcUper.isTrigger = true;
        }

        Rigidbody rbUper = SliceTools.FaultoleranceGetComponent<Rigidbody>(shatteredObject);

        if (rbUper != null)
        {
            if (jump)
            {
                rbUper.useGravity = false;
                rbUper.isKinematic = false;
            }
            else
            {
                rbUper.useGravity = false;
                rbUper.isKinematic = true;
            }
        }

        if (jump)
        {
            GameObject box = CreaateSliceBox(shatteredObject);

            rbUper.transform.DOJump(Vector3.up + transform.position, 4f, 1, 0.5f).OnComplete(() =>
            {
                MeshRenderer mrShatterObject = shatteredObject.GetComponent<MeshRenderer>();
                mrShatterObject.enabled = false;
                MeshRenderer mrBox = box.GetComponent<MeshRenderer>();
                mrBox.enabled = true;
            });
        }
    }

    private GameObject CreaateSliceBox(GameObject gameObject)
    {
        GameObject view = new GameObject("view");
        view.transform.parent = gameObject.transform;
        view.transform.position = gameObject.transform.position;
        GameObject sliceBox = Instantiate(_box, view.transform);
        sliceBox.name = "Box view";
        sliceBox.transform.position = view.transform.position;
        return sliceBox;
    }
}
