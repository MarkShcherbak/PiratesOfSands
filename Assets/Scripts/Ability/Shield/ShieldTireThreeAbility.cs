    using UnityEngine;

    public class ShieldTireThreeAbility : IAbility
    {
        public Color AbilityColor { get; set; }
        
        public ShieldTireThreeAbility()
        {
            AbilityColor = Color.blue;
        }

        public IAbility Add(IAbility ability)
        {
            if (ability is ShieldTierOneAbility) return new ShieldTierFourAbility();
            else return ability;
        }

        public void Execute(Transform position)
        {
            throw new System.NotImplementedException();
        }
    }
