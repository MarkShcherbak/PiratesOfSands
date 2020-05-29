using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFactory
{
    // Взрыв песка

    public static void CreateSandExplosion(Transform origin)
    {
        ParticleSystem particle = Resources.Load<ParticleSystem>("Particles/SandExplosion");
        UnityEngine.Object.Instantiate(particle, origin.position, Quaternion.identity);
    }

    public static void CreateShipCollision(Transform origin)
    {
        ParticleSystem particle = Resources.Load<ParticleSystem>("Particles/ShipCollision");
        UnityEngine.Object.Instantiate(particle, origin.position, Quaternion.identity);
    }

    public static void CreateShotSmoke(Transform origin)
    {
        ParticleSystem particle = Resources.Load<ParticleSystem>("Particles/ShotSmoke");
        UnityEngine.Object.Instantiate(particle, origin.position, Quaternion.identity);
    }
}