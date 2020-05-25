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
        shipMV.OnCollision += HandleCollision;

        shipMV.OnFlip += HandleFlip;
    }

    private void HandleCollision(object sender, GameObject e)
    {
        if(shipHPMV != null && shipMV.IsAlive)
        if (e.tag == "Projectile")
        {
            shipMV.Health -= 20; //TODO реализовать урон от проджектайла

            shipHPMV.GreenBarFill = (float)shipMV.Health/100;
            shipHPMV.HPAmount.text = $"{shipMV.Health}%";

                if (shipMV.Health <= 0) shipMV.IsAlive = false;

                MonoBehaviour.Destroy(e); //TODO корабль не должен удалять проджектайл!!
        }
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
        if (dotProduct < -0.8f)
        {
            shipMV.Rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
        }

        // Вручную вращаем корабль до тех пор, пока он не сможет вернуться "на ноги"
        // По сути следующее условие "вытаскивает" корабль вместе с коллайдером паруса из-под земли, если он там каким-то образом застрял
        // TODO не очень хорошо работает с текущим пилотом противника - крутит не ту ось =\

        if (dotProduct > 0.2f)
        {
            Debug.Log("Geez! Looks like a total flip!");

            Quaternion rotation = Quaternion.AngleAxis(shipMV.transform.rotation.z > 0 ? -5f : 5f, Vector3.forward);
            shipMV.Rigidbody.MoveRotation(shipMV.Rotation * rotation);

            if (dotProduct > 0.9f)
            {
                Debug.Log("Resetting...");
                shipMV.Rotation = Quaternion.identity;  // TODO сброс только по оси Z
            }
        }
    }
}