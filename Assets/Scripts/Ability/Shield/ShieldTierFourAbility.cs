    using UnityEngine;

    public class ShieldTierFourAbility : IAbility
    {
        public Color AbilityColor { get; set; }
        
        public ShieldTierFourAbility()
        {
            AbilityColor = Color.blue;
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
