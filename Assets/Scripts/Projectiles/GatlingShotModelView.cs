using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingShotModelView : MonoBehaviour
{
    #region Fields

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float damage;
    [SerializeField] private float speed;

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
        rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
    }
}
