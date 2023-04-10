using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToPosition : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = GetMousePosition();
            isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 movement = targetPosition - transform.position;
            rb.MovePosition(transform.position + movement.normalized * speed * Time.fixedDeltaTime);

            if (movement.magnitude < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return transform.position;
    }
}
