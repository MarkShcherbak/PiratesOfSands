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
            if (ability is ShieldTierOneAbility) return new ShieldTierThreeAbility();
            else return ability;
        }

        public void Execute(Transform position)
        {
        Debug.Log("Shield tire 2 launched!");
        }
    }
