using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetGrass : MonoBehaviour
{
    public static GetGrass instance;
    public Slider _sliderBack;
    public TMP_Text text;

    public List<PutGrassInBack> _grassArray = new List<PutGrassInBack>();

    private int _backValue;
    private bool IsFullBack;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
        _sliderBack.maxValue = 40f;
        text.text = $"{_sliderBack.value}/{_sliderBack.maxValue}";

    }

    public void GetGrasses()
    {
        //if (_sliderBack.value != _sliderBack.maxValue)
        //{
            foreach (Grass grass in Move.Instance._grasses)
            {
                if (grass != null && grass.isReady)
                {
                    Vector3 directionGrass = grass.transform.position - transform.position;
                    float angle = Vector3.Angle(directionGrass, transform.forward);
                    if (angle < 45f)
                    {
                        if (grass.isActiveAndEnabled)
                        {
                            grass.OnCreateHull(Vector3.up);
                            Move.Instance.StopChop();
                        }
                    }
                }
            }
        //}
    }
}
