using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonModelView : MonoBehaviour, ICannonModelView
{
    [SerializeField] private Transform shotOrigin;

    public IAbilityModelView LoadedAbility { get; set; }

    public void Fire()
    {
        Debug.Log("BOOM!");
    }


    //private IEnumerator Load()
    //{
    //    canShoot = false;
    //    yield return new WaitForSeconds(cannonType.fireRate);
    //    canShoot = true;
    //}

    //private Quaternion GetRandomInsideCone(float conicAngle)
    //{
    //    Quaternion randomTilt = Quaternion.AngleAxis(Random.Range(0f, conicAngle), Vector3.up);
    //    Quaternion randomSpin = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward);

    //    return (randomSpin * randomTilt);
    //}
}