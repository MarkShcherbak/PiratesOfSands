using System;
using UnityEngine;

// контроллер корабля
public class ShipController
{
    private readonly ShipModelView shipMV;
    private readonly HitpointsCanvasModelView shipHPMV;

    public ShipController(ShipModelView shipModelView, HitpointsCanvasModelView shipHPModelView)
    {
        shipHPMV = shipHPModelView;
        shipMV = shipModelView;
        shipMV.OnInput += HandleInput;
        shipMV.OnAction += HandleAction;

        shipMV.OnDamageRecieved += HandleRecieveDamage;

        //shipMV.OnHorizontalFlip += HandleHorizontalFlip;
        //shipMV.OnVerticalFlip += HandleVerticalFlip;
    }

    private void HandleAction(object sender, Vector3 direction)
    {
        if (direction != Vector3.back)
            shipMV.PrimaryAction(direction);

        else
            shipMV.SecondaryAction();
    }

    private void HandleInput(object sender, Vector3 input)
    {
        //TODO реализовать нормальное управление!!!
        if (shipMV.Rigidbody.velocity.magnitude < 15.0f)
            shipMV.Rigidbody.AddForce(shipMV.transform.forward * input.z * 1000 * Time.fixedDeltaTime * InputParams.moveTimeScale, ForceMode.Impulse);

        shipMV.transform.Rotate(0.0f, input.x, 0.0f);
    }

    private void HandleRecieveDamage(object sender, float amount)
    {
        if (shipHPMV != null)
        {
            shipMV.Health -= amount;

            shipHPMV.GreenBarFill = shipMV.Health / 100;
            shipHPMV.HPAmount.text = $"{shipMV.Health}%";

            if (shipMV.Health <= 0)
            {
                shipMV.IsAlive = false;
                shipHPMV.HPAmount.text = $"X_X";
                Debug.Log($"{shipMV.name} is destroyed!");
            }
        }
    }

    //private void HandleHorizontalFlip(object sender, float dotProduct)
    //{
    //    // Добавляем обратную тягу кораблю, чтобы вернуть его "на ноги"
    //    shipMV.Rigidbody.AddRelativeTorque(0, 0, shipMV.Rigidbody.angularVelocity.z * -2f, ForceMode.Acceleration);

    //    if (dotProduct < -0.6f)
    //        shipMV.Rigidbody.angularVelocity = new Vector3(
    //            x: shipMV.Rigidbody.angularVelocity.x,
    //            y: shipMV.Rigidbody.angularVelocity.y,
    //            z: shipMV.Rigidbody.angularVelocity.z / 2);
    //}

    //private void HandleVerticalFlip(object sender, float dotProduct)
    //{
    //    // Добавляем обратную тягу кораблю, чтобы вернуть его "на ноги"
    //    shipMV.Rigidbody.AddRelativeTorque(shipMV.Rigidbody.angularVelocity.x * -2f, 0, 0, ForceMode.Acceleration);

    //    if (dotProduct < 0.6f)
    //        shipMV.Rigidbody.angularVelocity = new Vector3(
    //            x: shipMV.Rigidbody.angularVelocity.x / 2,
    //            y: shipMV.Rigidbody.angularVelocity.y,
    //            z: shipMV.Rigidbody.angularVelocity.z);
    //}
}