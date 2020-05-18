using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EnemyPilotModelView : MonoBehaviour
{
    public event EventHandler OnMovingInput = (sender, e) => { };
    public event EventHandler<Vector3> OnActionInput = (sender, e) => { };
    public event EventHandler<String> OnTriggerCollision = (sender, tag) => { };

    private float moveH, moveV;
    private Vector3 actionDirection;
    
    //TODO добавить ИИ для управления и действий

    private void FixedUpdate()
    {
        OnMovingInput(this, EventArgs.Empty);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerCollision(this, other.tag);
    }
}