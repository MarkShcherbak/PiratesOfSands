using System.Collections;
using UnityEngine;

public class WeaponBoxModelView : MonoBehaviour
{
    public WeaponData[] containedWeapons;
    public bool isTaken = false;
    public float boxCooldown;

    public void OnTriggerEnter(Collider ship)
    {
        if (ship.CompareTag("Ship"))
        {
            if(isTaken == false)
            {
                WeaponData droppedWeapon = containedWeapons[Random.Range(0, containedWeapons.Length)];

                ship.GetComponent<ShipInventoryModelView>().weaponOnHold = droppedWeapon;

                Debug.Log($"Got the {droppedWeapon.type} cannon!");

                StartCoroutine(OnBoxTake(boxCooldown));
            }
        }
    }

    public IEnumerator OnBoxTake(float delay)
    {
        OpenBox();
        yield return new WaitForSeconds(delay);
        CloseBox();
    }

    public void CloseBox()
    {
        isTaken = false;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;

        Debug.Log("Box reappeared!");
    }

    public void OpenBox()
    {
        isTaken = true;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        Debug.Log("Box was opened!");
    }
}
