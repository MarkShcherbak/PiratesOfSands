using UnityEngine;

public class ShieldTierTwoAbility : IAbility
{
    public AbilityData Data { get; set; }

    public ShieldTierTwoAbility()
    {
        Data = Resources.Load<AbilityData>("AbilityData/Shield/TestShield");
    }

    public IAbility Add(IAbility ability)
    {
        if (ability is ShieldTierOneAbility) return new ShieldTierThreeAbility();
        else return ability;
    }

    public void Execute(Transform position)
    {
        Debug.Log("Shield tier 2 launched!");
    }
}
