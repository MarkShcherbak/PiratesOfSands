using UnityEngine;
using System.Collections.Generic;
using System;

public class ShipModelView : MonoBehaviour
{
    #region fields
    //Events
    public event EventHandler<Vector3> OnInput = (sender, e) => { };
    public event EventHandler<Vector3> OnAction = (sender, e) => { };
    public event EventHandler OnCollision = (other, e) => { };
    //Ship data
    [SerializeField] private Rigidbody rb;
    //Cannon spots on the ship sides
    [SerializeField] private List<Transform> frontSlots, backSlots, leftSlots, rightSlots;
    //Cannon arrays of Cannon types
    private ICannonModelView[] frontCannons, backCannons, leftCannons, rightCannons;

    //ShipAbility
    private IAbilityModelView abilitySlot;

    #endregion

    #region Accessors
    //Ability Accessor
    public IAbilityModelView Ability
    {
        get => abilitySlot;
        set => abilitySlot.Add(value);
    }

    //Rotation Accessor
    public Quaternion Rotation
    {
        get => rb.rotation;
        set
        {
            if(rb.rotation != value)
            {
                rb.rotation = value;
            }
        }
    }

    public Rigidbody Rigidbody { get => rb; }

    #endregion


    private void Start()
    {
        CreateCannons();
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

    public void Fire(Vector3 direction)
    {
        if (direction == Vector3.forward)
            foreach (ICannonModelView cannon in frontCannons)
                cannon.Fire();

        if (direction == Vector3.back)
            foreach (ICannonModelView cannon in backCannons)
                cannon.Fire();

        if (direction == Vector3.left)
            foreach (ICannonModelView cannon in leftCannons)
                cannon.Fire();

        if (direction == Vector3.right)
            foreach (ICannonModelView cannon in rightCannons)
                cannon.Fire();
    }


}
