﻿using UnityEngine;

public class SpeedFlyingBoostAbility : IAbility, ISecondary
{
    public AbilityData Data { get; set; }

    public SpeedFlyingBoostAbility()
    {
        Data = Resources.Load<AbilityData>("AbilityData/SpeedUp/SpeedFlyingBoost");
    }
    public IAbility Add(IAbility ability)
    {
        if (ability is SpeedSmallBoostAbility) return new SuperMegaWTFSpeedAbility();
        else return ability;
    }

    public void Execute(Transform position)
    {
        Debug.Log("Flying Speed Boost Launched!");

        SpeedUpFactory.CreateFlyingBoost(position, Data);
    }
}