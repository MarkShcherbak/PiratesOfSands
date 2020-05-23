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

    private void HandleFlip(object sender, float dotProduct)
    {
        Debug.Log("Uh-oh! Flipping on Z angle!");

        // Добавляем обратную тягу кораблю, чтобы вернуть его "на ноги"
        shipMV.Rigidbody.AddRelativeTorque(0, 0, shipMV.Rigidbody.angularVelocity.z * -2f, ForceMode.Acceleration);

        // Если корабль вернулся в приемлемый угол наклона - сбрасываем угловую скорость по оси Z, чтобы корабль не качало слишком сильно
        if (dotProduct < -0.8)
        {
            shipMV.Rigidbody.angularVelocity = new Vector3(shipMV.Rigidbody.angularVelocity.x, shipMV.Rigidbody.angularVelocity.y, 0);
        }

        // Вручную вращаем корабль до тех пор, пока он не сможет вернуться "на ноги"
        // По сути следующее условие "вытаскивает" корабль вместе с коллайдером паруса из-под земли, если он там каким-то образом 
        // TODO не очень хорошо работает с текущим пилотом противника

        if (dotProduct > 0.25f)
        {
            Quaternion rotation = Quaternion.AngleAxis(shipMV.transform.localEulerAngles.z > 0 ? -1f : 1f, Vector3.forward);
            shipMV.Rigidbody.MoveRotation(shipMV.Rigidbody.rotation * rotation);
        }
    }
}