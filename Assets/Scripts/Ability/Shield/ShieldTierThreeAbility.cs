    using UnityEngine;

    public class ShieldTierThreeAbility : IAbility
    {
 
        public AbilityData Data { get; set; }
        public ShieldTierThreeAbility()
        {
            Data = Resources.Load<AbilityData>("AbilityData/TestShield");
        }

        public IAbility Add(IAbility ability)
        {
            if (ability is ShieldTierOneAbility) return new ShieldTierFourAbility();
            else return ability;
        }

        public void Execute(Transform position)
        {
        Debug.Log("Shield tire 3 launched!");
        }
    }
