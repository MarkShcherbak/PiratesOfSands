    using UnityEngine;

    public class GrapplingHookAbility : IAbility
    {
        
        public AbilityData Data { get; set; }
        public GrapplingHookAbility()
        {
            Data = Resources.Load<AbilityData>("AbilityData/TestCannon");
        }
        public IAbility Add(IAbility ability)
        {
            if (ability is CannonballShotAbility) return new ChainShotAbility();
            else return ability;
        }

        public void Execute(Transform position)
        {
        Debug.Log("Hook fired!");
    }
        
    }
