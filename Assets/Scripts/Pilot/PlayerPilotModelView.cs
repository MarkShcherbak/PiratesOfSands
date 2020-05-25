using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerPilotModelView : MonoBehaviour
{
    public event EventHandler<Vector3> OnMovingInput = (sender, e) => { };
    public event EventHandler<Vector3> OnActionInput = (sender, e) => { };

    private void FixedUpdate()
    {
        OnMovingInput(this, new Vector3(InputParams.XAxis, 0, InputParams.ZAxis));
    }
}
