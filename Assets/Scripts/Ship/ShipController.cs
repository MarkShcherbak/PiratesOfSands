using System;
using UnityEngine;

// контроллер корабля
public class ShipController
{
    private readonly ShipModelView shipMV;
    private readonly HitpointsCanvasModelView shipHPMV;

    private float rbStartDrag;
    // Скалярная величина наклона корабля
    private float dotX;
    private float dotZ;

    private bool flipping = false;
    private float flipCooldown = 0f;

    /// <summary>
    /// Конструктор корабля
    /// </summary>
    /// <param name="shipModelView">Модель-представление этого корабля</param>
    /// <param name="shipHPModelView">Полоска ХП этого корабля</param>
    public ShipController(ShipModelView shipModelView, HitpointsCanvasModelView shipHPModelView)
    {
        shipHPMV = shipHPModelView;
        shipMV = shipModelView;

        rbStartDrag = shipMV.Rigidbody.drag;

        //EventHandlers
        shipMV.OnInput += HandleInput;
        shipMV.OnAction += HandleAction;
        shipMV.OnTriggerIN += HandleTriggerIN;
        shipMV.OnTriggerOUT += HandleTriggerOUT;
        shipMV.OnDamageRecieved += HandleRecieveDamage;
        shipMV.OnFixedUpdate += HandleFixedUpdate;

    }

    /// <summary>
    /// Обработка входа в зону триггера
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="tag"></param>
    private void HandleTriggerIN(object sender, string tag)
    {
        if (tag.Equals("SlowPoint"))
        {
            shipMV.Rigidbody.drag = rbStartDrag * 10;
        }

        if (tag.Equals("SlipperyPoint"))
        {
            switch (UnityEngine.Random.Range(0, 2))
            {
                case 0: shipMV.Rigidbody.AddRelativeTorque(new Vector3(0, -200f, 0), ForceMode.Impulse); break;
                case 1: shipMV.Rigidbody.AddRelativeTorque(new Vector3(0, 200f, 0), ForceMode.Impulse); break;
            }
        }
    }

    /// <summary>
    /// Обработка выхода из зоны триггера
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="tag"></param>
    private void HandleTriggerOUT(object sender, string tag)
    {
        if (tag.Equals("SlowPoint"))
        {
            shipMV.Rigidbody.drag = rbStartDrag;
        }
    }

    /// <summary>
    /// Обработка ввода действия
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="direction"></param>
    private void HandleAction(object sender, Vector3 direction)
    {
        if (direction != Vector3.back)
            shipMV.PrimaryAction(direction);

        else
            shipMV.SecondaryAction();
    }

    /// <summary>
    /// Обработка ввода управления
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="input"></param>
    private void HandleInput(object sender, Vector3 input)
    {
        Vector3 down = Vector3.Project(shipMV.Rigidbody.velocity, shipMV.transform.up);
        Vector3 right = Vector3.Project(shipMV.Rigidbody.velocity, -shipMV.transform.right);
        Vector3 forward = Vector3.Project(shipMV.Rigidbody.velocity, -shipMV.transform.forward);


        Ray ray = new Ray(shipMV.transform.position, -shipMV.transform.up);
        shipMV.transform.Rotate(0f, input.x * shipMV.shipDriveParams.RotateCoef * Time.fixedDeltaTime, 0f, Space.Self);
        if (Physics.Raycast(ray, shipMV.shipDriveParams.distance))
        {
            Vector3 direction = Vector3.ProjectOnPlane(shipMV.transform.forward, Vector3.up);
            shipMV.Rigidbody.AddForce(direction * input.z * shipMV.shipDriveParams.acceleration * InputParams.moveTimeScale, ForceMode.Acceleration);
            forward *= shipMV.shipDriveParams.inertialCoef;
            right *= shipMV.shipDriveParams.inertialCoef;
        }

        shipMV.Rigidbody.velocity = down + forward + right;

    }

    /// <summary>
    /// Обработка получения урона
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="amount"></param>
    private void HandleRecieveDamage(object sender, float amount)
    {
        if (shipHPMV != null)
        {
            shipMV.Health -= amount;
            Debug.Log($"{shipMV.name} was damaged for {amount} damage! {shipMV.Health} hp left!");

            shipHPMV.GreenBarFill = shipMV.Health / 100;
            shipHPMV.HPAmount.text = $"{shipMV.Health}%";

            if (shipMV.Health <= 0)
            {
                shipMV.IsAlive = false;
                shipHPMV.HPAmount.text = $"X_X";
                Debug.Log($"{shipMV.name} was destroyed!");

                //TODO переделать
                shipMV.DetachPilot();
            }
        }
    }

    /// <summary>
    /// Обработка FixedUpdate, проверка заваливания корабля TODO переделать иначе?
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HandleFixedUpdate(object sender, EventArgs e)
    {
        if (Time.time > flipCooldown)
        {
            flipping = false;
        }

        // Получаем скалярное произведение y+ и y- векторов корабля для того, чтобы получить уровень его наклона
        dotX = Vector3.Dot(shipMV.transform.up, Vector3.down);

        if(flipping == false)
        {
            if(dotX > -0.1f)
            {
                flipping = true;
                flipCooldown = Time.time + 1.0f;

                if (Physics.Raycast(shipMV.transform.position, shipMV.transform.right, 2f))
                {
                    shipMV.Rigidbody.AddExplosionForce(500f, shipMV.transform.position, 1f, 5f, ForceMode.Impulse);
                    shipMV.Rigidbody.AddRelativeTorque(shipMV.transform.InverseTransformDirection(shipMV.transform.forward) * 50f, ForceMode.Impulse);

                }

                else if (Physics.Raycast(shipMV.transform.position, -shipMV.transform.right, 2f))
                {
                    shipMV.Rigidbody.AddExplosionForce(500f, shipMV.transform.position, 1f, 5f, ForceMode.Impulse);
                    shipMV.Rigidbody.AddRelativeTorque(shipMV.transform.InverseTransformDirection(shipMV.transform.forward) * -50f, ForceMode.Impulse);
                }
            }
        }

        else
        {
            if(dotX < -0.8f)
            {
                shipMV.Rigidbody.angularVelocity *= 0.75f;
            }
        }
    }
}