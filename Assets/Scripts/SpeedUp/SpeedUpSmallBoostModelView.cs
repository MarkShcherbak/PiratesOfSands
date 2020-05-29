﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpSmallBoostModelView : MonoBehaviour, ISpeedUp
{
    [SerializeField] private float duration;
    [SerializeField] private float intensity;
    [SerializeField] private float maxSpeed;

    [SerializeField] private FixedJoint joint;
    [SerializeField] private Rigidbody rb;

    public float Duration
    {
        get => duration;
        set
        {
            if (duration != value)
            {
                duration = value;
            }
        }
    }

    public float Intensity
    {
        get => intensity;
        set
        {
            if (intensity != value)
            {
                intensity = value;
            }
        }
    }

    public float MaxSpeed
    {
        get => maxSpeed;
        set
        {
            if (maxSpeed != value)
            {
                maxSpeed = value;
            }
        }
    }

    private float destroyTime;

    private void Start()
    {
        joint.connectedBody = GetComponentInParent<ShipModelView>().Rigidbody;

        rb.AddRelativeForce(Vector3.back * intensity * 1000 * Time.fixedDeltaTime, ForceMode.Impulse);

        if (duration != 0)
            destroyTime = Time.time + duration;
    }

    private void Update()
    {
        if (destroyTime != 0)
        {
            if (Time.time > destroyTime)
                Destroy(gameObject);
        }

        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddRelativeForce(Vector3.back * intensity * 1000 * Time.fixedDeltaTime, ForceMode.Force);
        }
    }
}
