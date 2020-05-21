    using UnityEngine;

    public class ShieldTierOneAbility : IAbility
    {
        public AbilityData Data { get; set; }
        public ShieldTierOneAbility()
        {
            Data = Resources.Load<AbilityData>("AbilityData/TestShield");
        }

        

        public IAbility Add(IAbility ability)
        {
            if (ability is ShieldTierOneAbility) return new ShieldTierTwoAbility();
            else return ability;
        }

        public void Execute(Transform position)
        {
            Debug.Log("Shield Tire 1 Launched!");
        }
}
