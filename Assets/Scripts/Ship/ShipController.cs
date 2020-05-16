using System;
using UnityEngine;

// контроллер корабля
public class ShipController
    {
        private readonly ShipModelView shipMV;
        

        public ShipController(ShipModelView shipModelView)
        {
            shipMV = shipModelView;
            shipMV.OnInput += HandleInput;
            shipMV.OnAction += HandleAction;
        }

    private void HandleAction(object sender, Vector3 direction)
    {
        //todo реализовать действие

        //if not loaded - load, if loaded - fire
        shipMV.Fire(direction);
    }

    private void HandleInput(object sender, Vector3 input)
    {
        //TODO реализовать нормальное управление!!!
        if(shipMV.Rigidbody.velocity.magnitude < 50f)
        shipMV.Rigidbody.AddForce(shipMV.transform.forward * input.z * 1000 * Time.fixedDeltaTime,ForceMode.Acceleration);
        Debug.Log(shipMV.Rigidbody.velocity.magnitude);

        shipMV.transform.Rotate(0.0f, input.x * 2, 0.0f);
    }
}
