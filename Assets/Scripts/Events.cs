using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    public static Events Instance;
    public UnityAction CreateHull;

    [SerializeField] private GetGrass _getGrass;

    private Animator _animator;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void EventCreateHull() => _getGrass.GetGrasses();
}
