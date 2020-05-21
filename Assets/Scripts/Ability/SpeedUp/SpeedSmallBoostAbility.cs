using UnityEngine;

public class SpeedSmallBoostAbility : IAbility
{
    public AbilityData Data { get; set; }

    public SpeedSmallBoostAbility()
    {
        Data = Resources.Load<AbilityData>("AbilityData/SpeedUp/TestSpeedUp");
    }
    public IAbility Add(IAbility ability)
    {
        if (ability is SpeedSmallBoostAbility) return new SpeedBigBoostAbility();
        else return ability;
    }

    public void Execute(Transform position)
    {
        Debug.Log("Small speed Boost launched!");
    }
}
