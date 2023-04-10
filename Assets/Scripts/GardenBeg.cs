using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GardenBeg : MonoBehaviour
{
    [SerializeField] private GameObject _grass;
    [SerializeField] private GameObject[] _points;

    private void Start()
    {
        foreach (var point in _points)
        {
            GameObject obj = Instantiate(_grass, point.transform.position, Quaternion.identity);
            Grass grass = obj.GetComponent<Grass>();
        }
    }

    private void StartUpgrowth() => StartCoroutine(Upgrowth());

    IEnumerator Upgrowth()
    {
        yield return new WaitForSeconds(10);
        gameObject.SetActive(true);
    }
}
