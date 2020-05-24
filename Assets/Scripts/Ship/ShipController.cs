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

        shipMV.OnFlip += HandleFlip;
    }

    private void HandleAction(object sender, Vector3 direction)
    {
        if (direction != Vector3.back)
            shipMV.PrimaryAction(direction);
        else shipMV.SecondatyAction();
    }

    private void HandleInput(object sender, Vector3 input)
    {
        //TODO реализовать нормальное управление!!!
        if (shipMV.Rigidbody.velocity.magnitude < 15.0f)
            shipMV.Rigidbody.AddForce(shipMV.transform.forward * input.z * 1000 * Time.fixedDeltaTime, ForceMode.Acceleration);

        shipMV.transform.Rotate(0.0f, input.x, 0.0f);
    }

    private void HandleFlip(object sender, float dotProduct)
    {
        Debug.Log("Uh-oh! Flipping on Z angle!");

        // Добавляем обратную тягу кораблю, чтобы вернуть его "на ноги"
        shipMV.Rigidbody.AddRelativeTorque(0, 0, shipMV.Rigidbody.angularVelocity.z * -5f, ForceMode.Acceleration);

        // Если корабль вернулся в приемлемый угол наклона - сбрасываем угловую скорость по оси Z, чтобы корабль не качало слишком сильно
        if (dotProduct < -0.8)
        {
            shipMV.Rigidbody.angularVelocity = new Vector3(0, 0, 0);
        }

        // Вручную вращаем корабль до тех пор, пока он не сможет вернуться "на ноги"
        // По сути следующее условие "вытаскивает" корабль вместе с коллайдером паруса из-под земли, если он там каким-то образом застрял
        // TODO не очень хорошо работает с текущим пилотом противника - крутит не ту ось =\

        if (dotProduct > 0.3)
        {
            Debug.Log("Geez! Looks like a total flip!");

            Quaternion rotation = Quaternion.AngleAxis(shipMV.transform.rotation.z > 0 ? -5f : 5f, Vector3.forward);
            shipMV.Rigidbody.MoveRotation(shipMV.Rotation * rotation);

            if (dotProduct > 0.8)
            {
                Debug.Log("Resetting...");
                shipMV.Rotation = Quaternion.identity;  // TODO сброс только по оси Z
            }
        }
    }
}