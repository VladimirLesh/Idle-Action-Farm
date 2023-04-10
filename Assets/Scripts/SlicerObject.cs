using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using EzySlice;
using UnityEngine.Events;
using UnityEngine.UI;
using Plane = EzySlice.Plane;
using Random = UnityEngine.Random;

public class SlicerObject : MonoBehaviour
{
    public Material sliceMat1;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Plane1 plane))
        {
            Debug.Log("Plane");
            GameObject _source = plane.gameObject;
            if (_source != null)
            {
                SlicedHull _hull = SliceTools.SliceObject(_source, transform.position, Vector3.down);
                Debug.Log("OnCreateHull");
                if (_hull != null)
                {
                    GameObject uperHull = _hull.CreateUpperHull(_source, sliceMat1);
                    SliceObject(uperHull);
                }
            }
            Destroy(plane.gameObject);
        }

    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.TryGetComponent(out Plane1 plane))
        {
            // Debug-draw all contact points and normals
            foreach (ContactPoint contact in collisionInfo.contacts)
            {
                Debug.DrawRay(contact.point, contact.normal, Color.red);
            }
        }
    }

    private void SliceObject(GameObject shatteredObject)
    {
        if (shatteredObject == null)
            return;

        MeshCollider mcUper = SliceTools.FaultoleranceGetComponent<MeshCollider>(shatteredObject);
        if (mcUper != null)
        {
            mcUper.sharedMesh = shatteredObject.GetComponent<MeshFilter>().mesh;
            mcUper.convex = true;
            // mcUper.isTrigger = true;
        }

        Rigidbody rbUper = SliceTools.FaultoleranceGetComponent<Rigidbody>(shatteredObject);

        if (rbUper != null)
        {
            rbUper.useGravity = true;
        }
    }
    
}