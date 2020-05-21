using UnityEngine;
using System.Collections.Generic;
using System;

public class ShipModelView : MonoBehaviour
{
    #region fields
    //Events
    public event EventHandler<Vector3> OnInput = (sender, e) => { };
    public event EventHandler<Sprite> OnAbilityChanged = (sender, e) => { };
    public event EventHandler<Vector3> OnAction = (sender, e) => { };
    public event EventHandler OnCollision = (other, e) => { };
    //Ship data
    [SerializeField] private Rigidbody rb;
    //Cannon spots on the ship sides
    [SerializeField] private List<Transform> frontSlots, backSlots, leftSlots, rightSlots;
    //Cannon arrays of Cannon types
    private ICannonModelView[] frontCannons, backCannons, leftCannons, rightCannons;

    //ShipAbility
    private IAbility masterAbilitySlot;

    #endregion

    #region Accessors
    //Ability Accessor
    public IAbility MasterAbility
    {
        get => masterAbilitySlot;
        set
        {
            if (value == null)
                masterAbilitySlot = null;
             else if (masterAbilitySlot != null)
                masterAbilitySlot = masterAbilitySlot.Add(value);
             else masterAbilitySlot = value;

            if(masterAbilitySlot != null)
            OnAbilityChanged(this, masterAbilitySlot.Data.Icon);
            else OnAbilityChanged(this, null);
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
            if(rb.rotation != value)
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



    public void Action(Vector3 direction)
    {
        if (direction == Vector3.forward)
        {
            if(frontCannons[0].LoadedAbility != null)
            foreach (ICannonModelView cannon in frontCannons)
                { cannon.Fire(); cannon.LoadedAbility = null; }
            else if(masterAbilitySlot != null)
            {
                if (masterAbilitySlot.Data.EquippableFront)
                {
                    foreach (ICannonModelView cannon in frontCannons)
                        cannon.LoadedAbility = masterAbilitySlot;
                    Debug.Log(masterAbilitySlot + " is loaded to front cannon!");
                    masterAbilitySlot = null;
                }

                else
                {
                    Debug.Log("Ability can't be loaded on front!");
                }
            }       
        }
            

        if (direction == Vector3.back)
        {
            if (backCannons[0].LoadedAbility != null)
                foreach (ICannonModelView cannon in backCannons)
                { cannon.Fire(); cannon.LoadedAbility = null; }
            else if(masterAbilitySlot != null)
            {
                if (masterAbilitySlot.Data.EquippableBack)
                {
                    foreach (ICannonModelView cannon in backCannons)
                    cannon.LoadedAbility = masterAbilitySlot;
                Debug.Log(masterAbilitySlot + " is loaded to back cannon!");
                MasterAbility = null;
                }

                else
                {
                    Debug.Log("Ability can't be loaded on back!");
                }
            }
        }

        if (direction == Vector3.left)
        {
            if (leftCannons[0].LoadedAbility != null)
                foreach (ICannonModelView cannon in leftCannons)
                { cannon.Fire(); cannon.LoadedAbility = null; }
            else if (masterAbilitySlot != null)
            {
                if (masterAbilitySlot.Data.EquippableLeft)
                {
                    foreach (ICannonModelView cannon in leftCannons)
                        cannon.LoadedAbility = masterAbilitySlot;
                    Debug.Log(masterAbilitySlot + " is loaded to left cannon!");
                    MasterAbility = null;
                }

                else
                {
                    Debug.Log("Ability can't be loaded on left!");
                }
            }
        }
            

        if (direction == Vector3.right)
        {
            if (rightCannons[0].LoadedAbility != null)
                foreach (ICannonModelView cannon in rightCannons)
                { cannon.Fire(); cannon.LoadedAbility = null; }
            else if (masterAbilitySlot != null)
            {
                if (masterAbilitySlot.Data.EquippableLeft)
                {
                    foreach (ICannonModelView cannon in rightCannons)
                        cannon.LoadedAbility = masterAbilitySlot;
                    Debug.Log(masterAbilitySlot + " is loaded to right cannon!");
                    MasterAbility = null;
                }

                else
                {
                    Debug.Log("Ability can't be loaded on right!");
                }
            }
        }

        if (masterAbilitySlot != null)
            OnAbilityChanged(this, masterAbilitySlot.Data.Icon);
        else OnAbilityChanged(this, null);
    }


}
