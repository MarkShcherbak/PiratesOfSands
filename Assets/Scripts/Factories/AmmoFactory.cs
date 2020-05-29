using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoFactory
{
    // Обычное ядро

    public static void CreateCannonballShot(Transform origin, AbilityData data)
    {
        for (int i = 0; i < data.ProjectilesCount; i++)
        {
            GameObject shot = data.Prefab;
            CannonballShotModelView modelView = UnityEngine.Object.Instantiate(shot, origin.position, origin.rotation * GetRandomInsideCone(data.ProjectileScatter)).GetComponent<CannonballShotModelView>();
            modelView.Damage = data.ProjectileDamage;
            modelView.Speed = data.ProjectileSpeed;
        }
    }

    // Крюк-кошка

    public static void CreateHookShot(Transform origin, AbilityData data)
    {
        for (int i = 0; i < data.ProjectilesCount; i++)
        {
            GameObject shot = data.Prefab;
            HookShotModelView modelView = UnityEngine.Object.Instantiate(shot, origin.position, origin.rotation * GetRandomInsideCone(data.ProjectileScatter)).GetComponent<HookShotModelView>();
            modelView.Damage = data.ProjectileDamage;
            modelView.Speed = data.ProjectileSpeed;
        }
    }

    // Цепное ядро

    public static void CreateChainShot(Transform origin, AbilityData data)
    {
        for (int i = 0; i < data.ProjectilesCount; i++)
        {
            GameObject shot = data.Prefab;
            ChainShotModelView modelView = UnityEngine.Object.Instantiate(shot, origin.position, origin.rotation * GetRandomInsideCone(data.ProjectileScatter)).GetComponent<ChainShotModelView>();
            modelView.Damage = data.ProjectileDamage;
            modelView.Speed = data.ProjectileSpeed;
        }
    }

    // Картечь

    public static void CreateGatlingShot(Transform origin, AbilityData data)
    {
        for (int i = 0; i < data.ProjectilesCount; i++)
        {
            GameObject shot = data.Prefab;
            GatlingShotModelView modelView = UnityEngine.Object.Instantiate(shot, origin.position, origin.rotation * GetRandomInsideCone(data.ProjectileScatter)).GetComponent<GatlingShotModelView>();
            modelView.Damage = data.ProjectileDamage;
            modelView.Speed = data.ProjectileSpeed;
        }
    }

    private static Quaternion GetRandomInsideCone(float conicAngle)
    {
        Quaternion randomTilt = Quaternion.AngleAxis(Random.Range(0f, conicAngle), Vector3.up);
        Quaternion randomSpin = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward);

        return (randomSpin * randomTilt);
    }
}
