using UnityEngine;

public class NetAbility : IAbility
{
    public AbilityData Data { get; set; }

    public NetAbility()
    {
        Data = Resources.Load<AbilityData>("AbilityData/Hazard/TestHazard");
    }

    public IAbility Add(IAbility ability)
    {
        if (ability is OilAbility) return this;
        else return ability;
    }

    public void Execute(Transform position)
    {
        Debug.Log("Net launched!");
    }
}
