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

        //TODO подписаться на изменение Ability и добавить UI отображение!!
        }

    private void HandleAction(object sender, Vector3 direction)
    {
        shipMV.Action(direction);
    }

    private void HandleInput(object sender, Vector3 input)
    {
        //TODO реализовать нормальное управление!!!
        if(shipMV.Rigidbody.velocity.magnitude < 8.0f)
        shipMV.Rigidbody.AddForce(shipMV.transform.forward * input.z * 1000 * Time.fixedDeltaTime,ForceMode.Acceleration);

        shipMV.transform.Rotate(0.0f, input.x * 2, 0.0f);
    }
}
