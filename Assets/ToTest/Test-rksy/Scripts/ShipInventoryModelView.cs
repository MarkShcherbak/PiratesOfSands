using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipInventoryModelView : MonoBehaviour
{
    public List<Transform> frontSlots, backSlots, leftSlots, rightSlots;

    public WeaponData weaponOnHold;

    public void EquipWeapon(List<Transform> slotsToOperate)
    {
        if(weaponOnHold != null)
        {
            foreach (Transform slot in slotsToOperate)
            {
                if (slot.childCount == 0)
                {
                    //CannonFactory.CreateCannonModelView(slot, weaponOnHold);

                    Debug.Log($"Placed {weaponOnHold.type} cannon at {slot.name}!");
                }

                else
                {
                   // slot.GetComponentInChildren<CannonModelView>().cannonType = weaponOnHold;

                    Debug.Log($"Replaced {slot.name} with {weaponOnHold.type} cannon!");
                }
            }

            weaponOnHold = null;
        }

        else
        {
            Debug.Log("Nothing to equip at the moment...");
        }
    }
}
