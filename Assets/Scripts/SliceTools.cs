using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceTools : MonoBehaviour
{
    public static T FaultoleranceGetComponent<T>(GameObject go) where T : Component
    {
        if (go != null)
        {
            T component = go.GetComponent<T>();
            if (component == null)
            {
                component = go.AddComponent<T>();
            }
            return component;
        }
        return null;
    }
    
    public static SlicedHull SliceObject(GameObject obj, Vector3 positon, Vector3 normal,
        Material crossSectionMaterial = null)
    {
        if (obj.GetComponent<MeshFilter>() == null)
            return null;
        return obj.Slice(positon, normal, crossSectionMaterial);
    }
}
