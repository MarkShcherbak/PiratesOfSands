    using UnityEngine;

    public class SuperMegaWTFSpeedAbility : IAbility
    {
        public Color AbilityColor { get; set; }
        
        public SuperMegaWTFSpeedAbility()
        {
            AbilityColor = Color.green;
        }
        public IAbility Add(IAbility ability)
        {
            if (ability is SpeedSmallBoostAbility) return this;
            else return ability;
        }

        public void Execute(Transform position)
        {
        Debug.Log("SUPER MEGA SPEED BOOST LAUNCHED!");
        }
    }