    using UnityEngine;

    public class ShieldTierTwoAbility : IAbility
    {
        public Color AbilityColor { get; set; }
        
        public ShieldTierTwoAbility()
        {
            AbilityColor = Color.blue;
        }

        public IAbility Add(IAbility ability)
        {
            if (ability is ShieldTierOneAbility) return new ShieldTireThreeAbility();
            else return ability;
        }

        public void Execute(Transform position)
        {
            throw new System.NotImplementedException();
        }
    }
