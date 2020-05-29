﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballShotModelView : MonoBehaviour
{
    #region Fields

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    [SerializeField] private bool isHarmful;

    #endregion

    #region Accessors

    public Rigidbody Rigidbody { get => rb; }
    public float Damage
    {
        get => damage;
        set
        {
            if (damage != value)
            {
                damage = value;
            }
        }
    }

    public float Speed
    {
        get => speed;
        set
        {
            if (speed != value)
            {
                speed = value;
            }
        }
    }

    #endregion

    private void Start()
    {
        LaunchProjectile();
    }

    private void LaunchProjectile()
    {
        isHarmful = true;
        rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<MonoBehaviour>(out MonoBehaviour mb))
        {
            if (mb is IDamageable && isHarmful)
            {
                ((IDamageable)mb).RecieveDamage(Damage);
                Debug.Log($"{collision.collider.name} takes {damage} damage! from {name}");

                if (collision.collider.tag == "Ship")
                    ParticleFactory.CreateShipCollision(transform);
            }
        }

        else if (collision.collider.tag == "Ground")
        {
            ParticleFactory.CreateSandExplosion(transform);
        }

        isHarmful = false;
        rb.velocity /= 2f;
        StartCoroutine(DelayedDestroy(3f));
    }

    private IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.Object.Destroy(gameObject);
    }
}
