using UnityEngine;

public class ShieldTierThreeAbility : IAbility, ISecondary
{
    public AbilityData Data { get; set; }

    public ShieldTierThreeAbility()
    {
        Data = Resources.Load<AbilityData>("AbilityData/Shield/TestShield");
    }

    public IAbility Add(IAbility ability)
    {
        if (ability is ShieldTierOneAbility) return new ShieldTierFourAbility();
        else return ability;
    }

    public void Execute(Transform position)
    {
        Debug.Log("Shield tier 3 launched!");
    }
}
