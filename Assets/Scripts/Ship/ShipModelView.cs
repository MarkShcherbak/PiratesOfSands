using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

public class ShipModelView : MonoBehaviour, IDamageable
{
    #region fields
    //Events
    public event EventHandler<Vector3> OnInput = (sender, e) => { };
    public event EventHandler<Vector3> OnAction = (sender, e) => { }; 
    public event EventHandler<Sprite> OnPrimaryAbilityChanged = (sender, e) => { };
    public event EventHandler<Sprite> OnSecondaryAbilityChanged = (sender, e) => { };
    public event EventHandler<float> OnDamageRecieved = (sender, e) => { };

    //public event EventHandler<float> OnHorizontalFlip = (sender, e) => { };
    //public event EventHandler<float> OnVerticalFlip = (sender, e) => { };


    //Ship data
    [SerializeField] private Rigidbody rb;
    //Cannon spots on the ship sides
    [SerializeField] private List<Transform> frontSlots, backSlots, leftSlots, rightSlots;
    //Cannon arrays of Cannon types
    private ICannonModelView[] frontCannons, backCannons, leftCannons, rightCannons;

    // Shield spot on center of the ship
    [SerializeField] private Transform shieldSlot;

    //ShipAbility
    private IAbility primaryAbilitySlot;
    private IAbility secondaryAbilitySlot;

    // Скалярная величина наклона корабля
    private float dotX;
    private float dotZ;

    private float health = 100;
    private bool isAlive = true;

    #endregion

    private float startDrag;

    #region Accessors
    //IsAlive Accessor
    public bool IsAlive { get => isAlive; set => isAlive = value; }
    //Health Accessor
    public float Health
    {
        get => health; set => health = value;
    }
    //Ability Accessor
    public IAbility PrimaryAbility
    {
        get => primaryAbilitySlot;
        set
        {
            if (value == null)
                primaryAbilitySlot = null;
            else if (primaryAbilitySlot != null)
                primaryAbilitySlot = primaryAbilitySlot.Add(value);
            else primaryAbilitySlot = value;

            if (primaryAbilitySlot != null)
                OnPrimaryAbilityChanged(this, primaryAbilitySlot.Data.Icon);
            else OnPrimaryAbilityChanged(this, null);
        }
    }
    
    public IAbility SecondaryAbility
    {
        get => secondaryAbilitySlot;
        set
        {
            if (value == null)
                secondaryAbilitySlot = null;
            else if (secondaryAbilitySlot != null)
                secondaryAbilitySlot = secondaryAbilitySlot.Add(value);
            else secondaryAbilitySlot = value;

            if (secondaryAbilitySlot != null)
                OnSecondaryAbilityChanged(this, secondaryAbilitySlot.Data.Icon);
            else OnSecondaryAbilityChanged(this, null);
        }
    }

    //Shield slot accessor
    public Transform ShieldSlot { get => shieldSlot; }

    //Cannons accessor
    public ICannonModelView[] FrontCannons { get => frontCannons; }
    public ICannonModelView[] BackCannons { get => backCannons; }
    public ICannonModelView[] LeftCannons { get => leftCannons; }
    public ICannonModelView[] RightCannons { get => rightCannons; }

    //Rotation Accessor
    public Quaternion Rotation
    {
        get => rb.rotation;
        set
        {
            if (rb.rotation != value)
            {
                rb.rotation = value;
            }
        }
    }

    public Rigidbody Rigidbody { get => rb; }

    #endregion


    private void Awake()
    {
        startDrag = rb.drag;
        CreateCannons();

        // Устанавливаем центр тяжести корабля
        Rigidbody.centerOfMass = new Vector3(0, 0.5f, 0.5f);
    }

    #region input

    //dispatching input vector to the ship controller
    public void SteeringInput(Vector3 input)
    {
        if(isAlive)
        OnInput(this, input);
    }

    //dispatching action vector to the ship controller
    public void ActionInput(Vector3 input)
    {
        if (isAlive)
            OnAction(this, input);
    }
    

    #endregion

    //creating Cannons
    public void CreateCannons()
    {
        frontCannons = new ICannonModelView[frontSlots.Count];
        for (int i = 0; i < frontSlots.Count; i++)
            frontCannons[i] = CannonFactory.CreateCannonModelView(frontSlots[i]);

        //backCannons = new ICannonModelView[backSlots.Count];
        //for (int i = 0; i < backSlots.Count; i++)
        //    backCannons[i] = CannonFactory.CreateCannonModelView(backSlots[i]);

        leftCannons = new ICannonModelView[leftSlots.Count];
        for (int i = 0; i < leftSlots.Count; i++)
            leftCannons[i] = CannonFactory.CreateCannonModelView(leftSlots[i]);

        rightCannons = new ICannonModelView[rightSlots.Count];
        for (int i = 0; i < rightSlots.Count; i++)
            rightCannons[i] = CannonFactory.CreateCannonModelView(rightSlots[i]);

        ShieldFactory.CreateShieldTierFour(shieldSlot, Resources.Load<AbilityData>("AbilityData/Shields/ShieldTierFour"));
    }



    public void PrimaryAction(Vector3 direction)
    {
        if (primaryAbilitySlot != null)
        {
            if (direction == Vector3.forward)
            {
                foreach (ICannonModelView cannon in frontCannons)
                    cannon.Fire(primaryAbilitySlot);
            }

            if (direction == Vector3.left)
            {
                foreach (ICannonModelView cannon in leftCannons)
                    cannon.Fire(primaryAbilitySlot);
            }

            if (direction == Vector3.right)
            {
                foreach (ICannonModelView cannon in rightCannons)
                    cannon.Fire(primaryAbilitySlot);
            }

            //PrimaryAbility = null;
        }
    }

    public void SecondaryAction()
    {
        if (secondaryAbilitySlot != null)    // TODO: реализовать через Input system
        {
            // Обработка абилок щитов. TODO нормальная реализация (через интерфейс?)
            if (SecondaryAbility.Data.AbilityType == AbilityData.Ability.Shield)
            {
                // Удаляем щит из слота под щиты, если там что-то присутствует
                if (shieldSlot.childCount != 0)
                {
                    Destroy(shieldSlot.GetChild(0).gameObject);
                }

                SecondaryAbility.Execute(shieldSlot);
            }

            if (SecondaryAbility.Data.AbilityType == AbilityData.Ability.SpeedUp)
            {
                // Удаляем турбину из заднего слота, если там что-то присутствует
                foreach (Transform slot in backSlots)
                {
                    if (slot.childCount != 0)
                    {
                        Destroy(slot.GetChild(0).gameObject);
                    }

                    SecondaryAbility.Execute(slot);
                }
            }

            //SecondaryAbility = null;
        }
    }

    public void RecieveDamage(float amount)
    {
        if (isAlive)
            if (shieldSlot.childCount == 0)
                OnDamageRecieved(this, amount);
    }

    public void Update()
    {
        // Получаем скалярное произведение y+ и y- векторов корабля для того, чтобы получить уровень его наклона
        dotX = Vector3.Dot(transform.up, Vector3.down);
        dotZ = Math.Abs(Vector3.Dot(transform.forward, Vector3.down));

        if (dotX > -0.4f)
        {
            //OnHorizontalFlip(this, dotX);

            rb.AddRelativeTorque(0, 0, rb.angularVelocity.z * -5f, ForceMode.Impulse);

            if (dotX < -0.5f)
                rb.angularVelocity = new Vector3(
                    x: rb.angularVelocity.x,
                    y: rb.angularVelocity.y,
                    z: rb.angularVelocity.z / 2);
        }

        // TODO добавить флип по Z

        if (dotZ > 0.7f)
        {
            //OnVerticalFlip(this, dotZ);

            rb.AddRelativeTorque(rb.angularVelocity.x * -5f, 0, 0, ForceMode.Impulse);

            if (dotZ < 0.8f)
                rb.angularVelocity = new Vector3(
                    x: rb.angularVelocity.x / 2,
                    y: rb.angularVelocity.y,
                    z: rb.angularVelocity.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SlowPoint"))
        {
            rb.drag = startDrag * 10;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SlowPoint"))
        {
            rb.drag = startDrag;
        }
    }
}
