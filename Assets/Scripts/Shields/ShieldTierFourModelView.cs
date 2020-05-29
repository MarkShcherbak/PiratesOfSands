using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTierFourModelView : MonoBehaviour, IDamageable
{
    [SerializeField] private float duration;
    [SerializeField] private float durability;
    [SerializeField] private float damage;

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

    public float Durability
    {
        get => durability;
        set
        {
            if (durability != value)
            {
                durability = value;
            }
        }
    }

    private float destroyTime;

    private void Start()
    {
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

        if (durability <= 0)
        {
            Debug.Log($"{gameObject.name} was destroyed!");
            Destroy(gameObject);
        }
    }

    public void RecieveDamage(float amount)
    {
        durability -= amount;
        Debug.Log($"{gameObject.name} - {amount} dur. {durability} dur. left");
    }
}