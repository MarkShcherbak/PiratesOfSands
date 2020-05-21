using UnityEngine;

public class SpeedBigBoostAbility : IAbility
{

    public AbilityData Data { get; set; }

    public SpeedBigBoostAbility()
    {
        Data = Resources.Load<AbilityData>("AbilityData/SpeedUp/TestSpeedUp");
    }
    public IAbility Add(IAbility ability)
    {
        if (ability is SpeedSmallBoostAbility) return new SpeedFlyingBoostAbility();
        else return ability;
    }

    public void Execute(Transform position)
    {
        Debug.Log("Big Speed boost launched!");
    }
}
