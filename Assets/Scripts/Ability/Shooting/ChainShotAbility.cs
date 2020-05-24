using UnityEngine;

public class ChainShotAbility : IAbility,IPrimary
{
    public AbilityData Data { get; set; }

    public ChainShotAbility()
    {
        Data = Resources.Load<AbilityData>("AbilityData/Shooting/Chain");
    }
    public IAbility Add(IAbility ability)
    {
        if (ability is CannonballShotAbility) return new GatilngShotAbility();
        else return ability;
    }

    public void Execute(Transform position)
    {
        Debug.Log("Chain fired!");

        AmmoFactory.CreateChainShot(position, Data);
    }
}