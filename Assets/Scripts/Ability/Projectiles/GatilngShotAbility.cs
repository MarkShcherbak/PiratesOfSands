﻿using UnityEngine;

public class GatilngShotAbility : IAbility, IPrimary
{
    public Color AbilityColor { get; set; }
    public AbilityData Data { get; set; }

    public GatilngShotAbility()
    {
        AbilityColor = Color.red;
        Data = Resources.Load<AbilityData>("AbilityData/Projectiles/Gatling");
    }
    public IAbility Add(IAbility ability)
    {
        if (ability is CannonballShotAbility) return this;
        else return ability;
    }

    public void Execute(Transform position)
    {
        Debug.Log("Gatling fired!");

        AmmoFactory.CreateGatlingShot(position, Data);
        ParticleFactory.CreateShotSmoke(position);
    }
}
