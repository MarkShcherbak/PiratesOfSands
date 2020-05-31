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
                case 0: shipMV.Rigidbody.AddRelativeTorque(new Vector3(0, UnityEngine.Random.Range(-200f, -100f), 0), ForceMode.Impulse); break;
                case 1: shipMV.Rigidbody.AddRelativeTorque(new Vector3(0, UnityEngine.Random.Range(100f, 200f), 0), ForceMode.Impulse); break;
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
        //TODO реализовать нормальное управление!!!
        if (shipMV.Rigidbody.velocity.magnitude < 15.0f)
            shipMV.Rigidbody.AddForce(shipMV.transform.forward * input.z * 1000 * Time.fixedDeltaTime * InputParams.moveTimeScale, ForceMode.Impulse);

        shipMV.transform.Rotate(0.0f, input.x, 0.0f);
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
        // Получаем скалярное произведение y+ и y- векторов корабля для того, чтобы получить уровень его наклона
        dotX = Vector3.Dot(shipMV.transform.up, Vector3.down);
        dotZ = Math.Abs(Vector3.Dot(shipMV.transform.forward, Vector3.down));

        if (dotX > -0.4f)
        {
            shipMV.Rigidbody.AddRelativeTorque(0, 0, shipMV.Rigidbody.angularVelocity.z * -5f, ForceMode.Impulse);

            if (dotX < -0.5f)
                shipMV.Rigidbody.angularVelocity = new Vector3(
                    x: shipMV.Rigidbody.angularVelocity.x,
                    y: shipMV.Rigidbody.angularVelocity.y,
                    z: shipMV.Rigidbody.angularVelocity.z / 2);
        }
    
        if (dotZ > 0.5f)
        {
            shipMV.Rigidbody.AddForce(Vector3.down * 500 * Time.fixedDeltaTime, ForceMode.Acceleration);

            if (dotZ > 0.7f)
            {
                shipMV.Rigidbody.AddRelativeTorque(shipMV.Rigidbody.angularVelocity.x * -5f, 0, 0, ForceMode.Impulse);


                if (dotZ < 0.8f)
                    shipMV.Rigidbody.angularVelocity = new Vector3(
                        x: shipMV.Rigidbody.angularVelocity.x / 2,
                        y: shipMV.Rigidbody.angularVelocity.y,
                        z: shipMV.Rigidbody.angularVelocity.z);
            }
        }
    }
}