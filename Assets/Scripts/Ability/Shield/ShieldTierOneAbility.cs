    using UnityEngine;

    public class ShieldTierOneAbility : IAbility
    {

        public ShieldTierOneAbility()
        {
            AbilityColor = Color.blue;
        }

        public Color AbilityColor { get; set; }

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
