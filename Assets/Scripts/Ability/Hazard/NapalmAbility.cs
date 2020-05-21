using UnityEngine;

public class NapalmAbility : IAbility
{
    public AbilityData Data { get; set; }

    public NapalmAbility()
    {
        Data = Resources.Load<AbilityData>("AbilityData/Hazard/TestHazard");
    }

    public IAbility Add(IAbility ability)
    {
        if (ability is OilAbility) return new NetAbility();
        else return ability;
    }


    public void Execute(Transform position)
    {
        Debug.Log("Napalm launched!");
    }
}
