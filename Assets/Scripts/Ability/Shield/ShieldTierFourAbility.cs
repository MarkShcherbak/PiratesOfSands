using UnityEngine;

public class ShieldTierFourAbility : IAbility
{
    public AbilityData Data { get; set; }

    public ShieldTierFourAbility()
    {
        Data = Resources.Load<AbilityData>("AbilityData/Shield/TestShield");
    }

    public IAbility Add(IAbility ability)
    {
        if (ability is ShieldTierOneAbility) return this;
        else return ability;
    }

    public void Execute(Transform position)
    {
        Debug.Log("Shield tier 4 Launched!");
    }
}
