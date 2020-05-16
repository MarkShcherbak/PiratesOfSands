using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerPilotModelView : MonoBehaviour
{
    public event EventHandler<Vector3> OnMovingInput = (sender, e) => { };
    public event EventHandler<Vector3> OnActionInput = (sender, e) => { };

    private float moveH, moveV;
    private Vector3 actionDirection;
    void Update()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");

        actionDirection = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.UpArrow)) actionDirection = Vector3.forward;
        if (Input.GetKeyDown(KeyCode.DownArrow)) actionDirection = Vector3.back;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) actionDirection = Vector3.left;
        if (Input.GetKeyDown(KeyCode.RightArrow)) actionDirection = Vector3.right;

        if (actionDirection != Vector3.zero)
            OnActionInput(this, actionDirection);
    }

    private void FixedUpdate()
    {
        if (moveH != 0 || moveV != 0)
            OnMovingInput(this, new Vector3(moveH, 0, moveV));
    }
}
