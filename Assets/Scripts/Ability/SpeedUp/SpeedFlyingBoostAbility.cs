    using UnityEngine;

    public class SpeedFlyingBoostAbility : IAbility
    {
        public Color AbilityColor { get; set; }
        
        public SpeedFlyingBoostAbility()
        {
            AbilityColor = Color.green;
        }
        public IAbility Add(IAbility ability)
        {
            if (ability is SpeedSmallBoostAbility) return new SuperMegaWTFSpeedAbility();
            else return ability;
        }

        public void Execute(Transform position)
        {
            throw new System.NotImplementedException();
        }
    }
