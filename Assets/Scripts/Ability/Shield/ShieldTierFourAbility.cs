    using UnityEngine;

    public class ShieldTierFourAbility : IAbility
    {

        public AbilityData Data { get; set; }
        public ShieldTierFourAbility()
        {
            Data = Resources.Load<AbilityData>("AbilityData/TestShield");
        }

        public IAbility Add(IAbility ability)
        {
            if (ability is ShieldTierOneAbility) return this;
            else return ability;
        }

        public void Execute(Transform position)
        {
            Debug.Log("Shield Tire 4 Launched!");
        }
    }
