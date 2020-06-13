using System;
using UnityEngine;

// контроллер корабля
public class ShipController
{
    private readonly ShipModelView shipMV;
    private readonly HitpointsCanvasModelView shipHPMV;

    private float rbStartDrag;
    // Скалярная величина наклона корабля
    private float dotZ;
    private float dotX;

    private bool flipping = false;
    private float flipCooldown = 0f;

    private float previousAngularDrag;

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

        previousAngularDrag = shipMV.Rigidbody.angularDrag;

    }

    /// <summary>
    /// Обработка входа в зону триггера
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="tag"></param>
    private void HandleTriggerIN(object sender, string tag)
    {
        if (shipMV.ShieldSlot.childCount == 0)
        {
            if (tag.Equals("SlowPoint"))
            {
                shipMV.Rigidbody.velocity *= 0.25f;
            }

            if (tag.Equals("SlipperyPoint"))
            {
                
                switch (UnityEngine.Random.Range(0, 2))
                {
                    case 0: shipMV.Rigidbody.AddRelativeTorque(new Vector3(0, -1000f, 0), ForceMode.Impulse); break;
                    case 1: shipMV.Rigidbody.AddRelativeTorque(new Vector3(0, 1000f, 0), ForceMode.Impulse); break;
                }
            }

            if (tag.Equals("Tornado"))
            {
                shipMV.Rigidbody.AddForce(Vector3.up * 250f, ForceMode.Impulse);
                shipMV.Rigidbody.AddRelativeTorque(Vector3.up * UnityEngine.Random.Range(-1000f, 1000f), ForceMode.Impulse);

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
        //if (tag.Equals("SlowPoint"))
        //{
        //    shipMV.Rigidbody.drag = rbStartDrag;
        //}
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


        Ray ray = new Ray(shipMV.transform.position, Vector3.down);
        shipMV.transform.Rotate(0f, input.x * shipMV.shipDriveParams.RotateCoef * Time.fixedDeltaTime, 0f, Space.Self);
        shipMV.isOnGround = Physics.Raycast(ray, shipMV.shipDriveParams.distance);
        if (shipMV.isOnGround)
        {
            shipMV.Rigidbody.angularDrag = previousAngularDrag;

            Vector3 direction = Vector3.ProjectOnPlane(shipMV.transform.forward, Vector3.up);
            shipMV.Rigidbody.AddForce(direction * input.z * shipMV.shipDriveParams.acceleration * InputParams.moveTimeScale, ForceMode.Acceleration);
            forward *= shipMV.shipDriveParams.inertialCoef;
            right *= shipMV.shipDriveParams.inertialCoef;

            if (shipMV.DustTrail.isStopped)
                shipMV.DustTrail.Play(true);
        }
        else
        {
            shipMV.Rigidbody.angularDrag = 3f;

            if(shipMV.DustTrail.isPlaying)
                shipMV.DustTrail.Stop(true);
            
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
            if(shipMV.ShieldSlot.childCount == 0)
            {
                shipMV.Health -= amount;

                shipHPMV.GreenBarFill = shipMV.Health / 100;
                shipHPMV.HPAmount.text = $"{shipMV.Health}%";

                if (shipMV.Health <= 0)
                {
                    shipMV.IsAlive = false;
                    shipHPMV.HPAmount.text = $"X_X";
                    Debug.Log($"{shipMV.name} was destroyed!");

                    //TODO переделать
                    //shipMV.DetachPilot();
                }
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
        if (Time.time < flipCooldown)
        {
            flipping = true;
        }

        else
        {
            flipping = false;
        }

        // Получаем скалярное произведение y+ и y- векторов корабля для того, чтобы получить уровень его наклона
        dotZ = Vector3.Dot(shipMV.transform.up, Vector3.down);
        dotX = Vector3.Dot(shipMV.transform.forward, Vector3.down);

        if (flipping == false && shipMV.IsAlive)
        {
            if (Physics.Raycast(shipMV.transform.position, Vector3.down, 5f, 1 << LayerMask.NameToLayer("Ground")))
            {
                if (dotZ > -0.1f)
                    FlipShip(shipMV.transform.forward);

                if (dotX > 0.7f)
                        FlipShip(shipMV.transform.up);

                if (dotX < -0.7f)
                        FlipShip(-shipMV.transform.up);
            }
        }
    }

    private void FlipShip(Vector3 direction)
    {
        flipCooldown = Time.time + 3.0f;

        shipMV.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        shipMV.transform.position += Vector3.up * 0.5f;
        shipMV.Rigidbody.velocity = Vector3.zero;
        shipMV.Rigidbody.angularVelocity = Vector3.zero;
    }
}