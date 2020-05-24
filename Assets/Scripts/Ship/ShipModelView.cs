using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

public class ShipModelView : MonoBehaviour
{
    #region fields
    //Events
    public event EventHandler<Vector3> OnInput = (sender, e) => { };
    public event EventHandler<Vector3> OnAction = (sender, e) => { }; 
    public event EventHandler<Sprite> OnPrimaryAbilityChanged = (sender, e) => { };
    public event EventHandler<Sprite> OnSecondaryAbilityChanged = (sender, e) => { };
    public event EventHandler OnCollision = (other, e) => { };

    public event EventHandler<float> OnFlip = (sender, e) => { };

    //Ship data
    [SerializeField] private Rigidbody rb;
    //Cannon spots on the ship sides
    [SerializeField] private List<Transform> frontSlots, backSlots, leftSlots, rightSlots;
    //Cannon arrays of Cannon types
    private ICannonModelView[] frontCannons, backCannons, leftCannons, rightCannons;

    //ShipAbility
    private IAbility primaryAbilitySlot;
    private IAbility secondaryAbilitySlot;

    // Скалярная величина наклона корабля
    private float dotUp;

    #endregion

    #region Accessors
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
        CreateCannons();

        // Устанавливаем центр тяжести корабля
        Rigidbody.centerOfMass = new Vector3(Rigidbody.centerOfMass.x, 0, Rigidbody.centerOfMass.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollision(collision.gameObject, EventArgs.Empty);
    }

    #region input

    //dispatching input vector to the ship controller
    public void SteeringInput(Vector3 input)
    {
        OnInput(this, input);
    }

    //dispatching action vector to the ship controller
    public void ActionInput(Vector3 input)
    {
        OnAction(this, input);
    }
    

    #endregion

    //creating Cannons
    public void CreateCannons()
    {
        frontCannons = new ICannonModelView[frontSlots.Count];
        for (int i = 0; i < frontSlots.Count; i++)
            frontCannons[i] = CannonFactory.CreateCannonModelView(frontSlots[i]);

        backCannons = new ICannonModelView[backSlots.Count];
        for (int i = 0; i < backSlots.Count; i++)
            backCannons[i] = CannonFactory.CreateCannonModelView(backSlots[i]);

        leftCannons = new ICannonModelView[leftSlots.Count];
        for (int i = 0; i < leftSlots.Count; i++)
            leftCannons[i] = CannonFactory.CreateCannonModelView(leftSlots[i]);

        rightCannons = new ICannonModelView[rightSlots.Count];
        for (int i = 0; i < rightSlots.Count; i++)
            rightCannons[i] = CannonFactory.CreateCannonModelView(rightSlots[i]);
    }



    public void PrimaryAction(Vector3 direction)
    {
        if (direction == Vector3.forward)
        {
            if (primaryAbilitySlot != null)
                foreach (ICannonModelView cannon in frontCannons)
                    cannon.Fire(primaryAbilitySlot);

            PrimaryAbility = null;
        }
        
        if (direction == Vector3.left)
        {
            if(primaryAbilitySlot != null)
                foreach (ICannonModelView cannon in leftCannons)
                    cannon.Fire(primaryAbilitySlot);

            PrimaryAbility = null;
        }
        
        if (direction == Vector3.right)
        {
            if(primaryAbilitySlot != null)
                foreach (ICannonModelView cannon in rightCannons)
                    cannon.Fire(primaryAbilitySlot);

            PrimaryAbility = null;
        }
    }

    public void SecondatyAction()
    {
        if (secondaryAbilitySlot != null)
        {
            foreach (ICannonModelView cannon in backCannons)
            {
                cannon.Fire(secondaryAbilitySlot);
            }

            SecondaryAbility = null;
        }
    }




    public void Update()
    {
        // Получаем скалярное произведение y+ и y- векторов корабля для того, чтобы получить уровень его наклона
        dotUp = Vector3.Dot(transform.up, Vector3.down);

        if (dotUp > -0.6f)
        {
            OnFlip(this, dotUp);
        }
    }
}
