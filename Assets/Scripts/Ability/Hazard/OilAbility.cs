using UnityEngine;

public class OilAbility : IAbility
{
    public AbilityData Data { get; set; }

    public OilAbility()
    {
        Data = Resources.Load<AbilityData>("AbilityData/Hazard/TestHazard");
    }

    public IAbility Add(IAbility ability)
    {
        if (ability is OilAbility) return new BombAbility();
        else return ability;
    }

    public void Execute(Transform position)
    {
        Debug.Log("Oil launched!");
    }
}
