using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerShootingControl : MonoBehaviour
{
    public ShipInventoryModelView inventory;

    public void EquipementControl(List<Transform> slotsToOperate)
    {
        if(Input.GetKey(KeyCode.E) == true)
        {
            inventory.EquipWeapon(slotsToOperate);
        }
    }

    public void ShootingControl(List<Transform> slotsToOperate)
    {
        if (Input.GetKey(KeyCode.E) == false)
        {
            foreach (Transform slot in slotsToOperate)
            {
                if (slot.childCount != 0)
                {
                    slot.GetComponentInChildren<CannonModelView>().Shoot();
                }

                else
                {
                    Debug.Log($"Denied! No cannon installed at {slot.name}!");
                }
            }
        }
    }

    public void Update()
    {
        // Нос
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            EquipementControl(inventory.frontSlots);
            ShootingControl(inventory.frontSlots);
        }

        // Корма
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            EquipementControl(inventory.backSlots);
            ShootingControl(inventory.backSlots);
        }

        // Левый борт
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            EquipementControl(inventory.leftSlots);
            ShootingControl(inventory.leftSlots);
        }

        // Правый борт
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            EquipementControl(inventory.rightSlots);
            ShootingControl(inventory.rightSlots);
        }
    }
}
