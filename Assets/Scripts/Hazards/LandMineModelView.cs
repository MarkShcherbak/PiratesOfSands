using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineModelView : MonoBehaviour
{
    #region Fields
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private float force;
    [SerializeField] private float radius;

    private bool isArmed;
    private float destroyTime;

    private Collider[] affectedColliders;
    private HashSet<GameObject> affectedObjects;

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

    public float Lifetime
    {
        get => lifetime;
        set
        {
            if (lifetime != value)
            {
                lifetime = value;
            }
        }
    }

    public float Force
    {
        get => force;
        set
        {
            if (force != value)
            {
                force = value;
            }
        }
    }

    public float Radius
    {
        get => radius;
        set
        {
            if (radius != value)
            {
                radius = value;
            }
        }
    }

    #endregion

    private void Start()
    {
        isArmed = false;
        affectedObjects = new HashSet<GameObject>();

        if (lifetime != 0)
            destroyTime = Time.time + lifetime;

        LaunchHazard();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void Update()
    {
        if (destroyTime != 0)
        {
            affectedColliders = Physics.OverlapSphere(transform.position, radius);

            if (Time.time > destroyTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Ship"))
        {
            if (isArmed)
            {
                Explode();
            }
        }

        else if (collision.collider.tag.Equals("Ground"))
        {
            isArmed = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void LaunchHazard()
    {
        rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
    }

    private void Explode()
    {
        if (affectedColliders != null)
        {
            foreach (Collider collider in affectedColliders)
            {
                if (collider && affectedObjects.Contains(collider.gameObject) == false)
                {
                    affectedObjects.Add(collider.gameObject);
                }
            }

            foreach (GameObject obj in affectedObjects)
            {
                if (obj.TryGetComponent<MonoBehaviour>(out MonoBehaviour mb))
                {
                    if (mb is IDamageable)
                    {
                        ((IDamageable)mb).RecieveDamage(damage);

                        mb.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, 10f, ForceMode.Impulse);
                    }
                }
            }
        }

        ParticleFactory.CreateSmallExplosion(transform);
        Destroy(gameObject);
    }
}
