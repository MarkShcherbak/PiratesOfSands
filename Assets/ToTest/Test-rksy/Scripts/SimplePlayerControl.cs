using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerControl : MonoBehaviour
{
    private Rigidbody rb;

    public float movementSpeed;
    public float rotationSpeed;

    public float speedMultiplier;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float movement = Input.GetAxis("Vertical");
        float direction = Input.GetAxis("Horizontal");

        rb.AddRelativeForce(Vector3.forward * movement * movementSpeed * speedMultiplier * Time.fixedDeltaTime);
        rb.AddRelativeTorque(Vector3.up * direction * rotationSpeed * speedMultiplier * Time.fixedDeltaTime);
    }
}
