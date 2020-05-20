    using UnityEngine;

    public class ShieldTierThreeAbility : IAbility
    {
        public Color AbilityColor { get; set; }
        
        public ShieldTierThreeAbility()
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
        Debug.Log("Shield tire 3 launched!");
        }
    }
