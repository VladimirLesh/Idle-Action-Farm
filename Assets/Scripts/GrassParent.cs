using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassParent : MonoBehaviour
{
    private Grass _grass;

    IEnumerator BurnGrass()
    {
        yield return new WaitForSeconds(10);
        Stump stump = GetComponentInChildren<Stump>();
        if (_grass != null)
        {
            _grass.view.SetActive(true);
            _grass.isReady = true;
        }
        
        Destroy(stump.gameObject);
    }

    public void StartCor()
    {
        StartCoroutine(BurnGrass());
    }

    public void SetGrass(Grass grass) => _grass = grass;
}
