    using UnityEngine;

    public class GatilngShotAbility : IAbility
    {
        
        public AbilityData Data { get; set; }
        public GatilngShotAbility()
        {
            Data = Resources.Load<AbilityData>("AbilityData/TestCannon");
        }
        public IAbility Add(IAbility ability)
        {
            if (ability is CannonballShotAbility) return this;
            else return ability;
        }

        public void Execute(Transform position)
        {
        Debug.Log("Gatling fired!");
    }
    }
