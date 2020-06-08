using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardFactory
{
    // Масло

    public static void CreateOilHazard(Transform origin, AbilityData data)
    {
        float step = data.HazardScatter / (data.HazardsCount - 1);
        float currentSpawnRotation;

        if (data.HazardsCount > 1)
            currentSpawnRotation = (origin.rotation.y - data.HazardScatter) / 2;

        else
            currentSpawnRotation = 0f;

        for (int i = 0; i < data.HazardsCount; i++)
        {
            GameObject hazard = data.Prefab;
            OilModelView modelView = UnityEngine.Object.Instantiate
                (hazard, origin.position + origin.forward * 2.5f, origin.rotation * Quaternion.Euler(-15f, currentSpawnRotation, 0f)).GetComponent<OilModelView>();

            modelView.Damage = data.HazardDamage;
            modelView.Speed = data.HazardSpeed;
            modelView.Lifetime = data.HazardLifetime;

            currentSpawnRotation += step;
        }
    }

    // Шипы

    public static void CreateSpikesHazard(Transform origin, AbilityData data)
    {
        float step = data.HazardScatter / (data.HazardsCount - 1);
        float currentSpawnRotation;

        if (data.HazardsCount > 1)
            currentSpawnRotation = (origin.rotation.y - data.HazardScatter) / 2;

        else
            currentSpawnRotation = 0f;

        for (int i = 0; i < data.HazardsCount; i++)
        {
            GameObject hazard = data.Prefab;
            SpikeModelView modelView = UnityEngine.Object.Instantiate
                (hazard, origin.position + origin.forward * 2.5f, origin.rotation * Quaternion.Euler(-15f, currentSpawnRotation, 0f))
                .GetComponent<SpikeModelView>();

            modelView.Damage = data.HazardDamage;
            modelView.Speed = data.HazardSpeed;
            modelView.Lifetime = data.HazardLifetime;

            currentSpawnRotation += step;
        }
    }

    // Мины

    public static void CreateLandMineHazard(Transform origin, AbilityData data)
    {
        float step = data.HazardScatter / (data.HazardsCount - 1);
        float currentSpawnRotation;

        if (data.HazardsCount > 1)
            currentSpawnRotation = (origin.rotation.y - data.HazardScatter) / 2;

        else
            currentSpawnRotation = 0f;

        for (int i = 0; i < data.HazardsCount; i++)
        {
            GameObject hazard = data.Prefab;
            LandMineModelView modelView = UnityEngine.Object.Instantiate
                (hazard, origin.position + origin.forward * 2.5f, origin.rotation * Quaternion.Euler(0f, currentSpawnRotation, 0f))
                .GetComponent<LandMineModelView>();

            modelView.Damage = data.HazardDamage;
            modelView.Speed = data.HazardSpeed;
            modelView.Lifetime = data.HazardLifetime;
            modelView.Force = data.HazardExplosionForce;
            modelView.Radius = data.HazardExplosionRadius;

            currentSpawnRotation += step;
        }
    }

    // Бомба

    public static void CreateBigBombHazard(Transform origin, AbilityData data)
    {
        float step = data.HazardScatter / (data.HazardsCount - 1);
        float currentSpawnRotation;

        if (data.HazardsCount > 1)
            currentSpawnRotation = (origin.rotation.y - data.HazardScatter) / 2;

        else
            currentSpawnRotation = 0f;

        for (int i = 0; i < data.HazardsCount; i++)
        {
            GameObject hazard = data.Prefab;
            BigBombModelView modelView = UnityEngine.Object.Instantiate
                (hazard, origin.position + origin.forward * 2.5f, origin.rotation * Quaternion.Euler(-15f, currentSpawnRotation, 0f))
                .GetComponent<BigBombModelView>();

            modelView.Damage = data.HazardDamage;
            modelView.Speed = data.HazardSpeed;
            modelView.Lifetime = data.HazardLifetime;
            modelView.Force = data.HazardExplosionForce;
            modelView.Radius = data.HazardExplosionRadius;

            currentSpawnRotation += step;
        }
    }

    // Масляное пятно
    public static GameObject CreateOilSplatter(Transform origin)
    {
        GameObject splatter = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Hazard/OilSplatter"), origin.position, Quaternion.Euler(0, UnityEngine.Random.Range(0, 359f), 0));
        return splatter;
    }
}
