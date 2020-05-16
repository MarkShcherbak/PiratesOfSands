using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RKSYCannonModelView : MonoBehaviour
{
    [SerializeField] private Transform shotOrigin;
    public WeaponData cannonType;

    private bool canShoot = true;

    public void Fire()
    {
        if(canShoot)
        {
            for (int i = 0; i < cannonType.projectilesAtOnce; i++)
            {
                GameObject projectile = Instantiate(
                    cannonType.projectile,
                    shotOrigin.position,
                    shotOrigin.rotation * GetRandomInsideCone(cannonType.projectileScatter)
                    );

                if (projectile.TryGetComponent(out ProjectileModelView projectileModelView))
                {
                    projectileModelView.Shoot(cannonType.projectileSpeed);
                }

                else
                {
                    Debug.Log("Projectile doesn't have any projectile behaviour on it. Check if projectile object is correct!");
                }
            }

            // Запуск сопрограммы перезарядки пушки
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(cannonType.fireRate);
        canShoot = true;
    }

    private Quaternion GetRandomInsideCone(float conicAngle)
    {
        Quaternion randomTilt = Quaternion.AngleAxis(Random.Range(0f, conicAngle), Vector3.up);
        Quaternion randomSpin = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward);

        return (randomSpin * randomTilt);
    }
}