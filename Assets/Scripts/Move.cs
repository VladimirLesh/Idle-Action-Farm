using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class Move : MonoBehaviour
{
    public FloatingJoystick joystick;
    public static Move Instance;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;
    [SerializeField] private GameObject _tool;
    [SerializeField] private GameObject joystickBG;
    [SerializeField] private GetGrass _getGrass;
    public GameObject _back;

    public Animator _animator;
    private Rigidbody _rb;
    public bool isChop = false;
    public List<Grass> _grasses = new List<Grass>();

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
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Grass grass))
        {
            _grasses.Add(grass);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if (_getGrass._sliderBack.value != _getGrass._sliderBack.maxValue)
        //{
            if (other.gameObject.TryGetComponent(out Grass grass))
            {
                if (!grass.isReady)
                {
                    return;
                }
                Vector3 directionGrass = grass.transform.position - transform.position;
                float angle = Vector3.Angle(directionGrass, transform.forward);
                if (angle < 90f)
                {
                    isChop = true;
                    _tool.SetActive(true);
                }
            }
        //}
    }

    public void StopChop() => isChop = false;

    private void OnTriggerExit(Collider other)
    {
        isChop = false;
        _tool.SetActive(false);
        
        if (other.TryGetComponent(out Grass grass))
        {
            _grasses.Remove(grass);
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void Update()
    {
        if (isChop) _animator.Play("Chop");
        if (joystickBG.activeInHierarchy)
        {
            _animator.Play("Run Tree");
            RunWithTool(0);
        }
        else
        {
            _animator.Play("Idle");
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        }
    }

    private void MoveCharacter()
    {
        Vector3 movement = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        if (movement != Vector3.zero)
        {
            _animator.Play("Run Tree");
        }
        _rb.velocity = movement * _speed;

        if (_rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, 
                Quaternion.LookRotation(_rb.velocity, Vector3.up), _speedRotation);
        }
    }
    
    public void RunWithTool(float value)
    {
        _animator.SetFloat("Tool", value);
    }
}
