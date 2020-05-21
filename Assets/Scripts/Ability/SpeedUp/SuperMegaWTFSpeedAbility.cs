    using UnityEngine;

    public class SuperMegaWTFSpeedAbility : IAbility
    {
        public AbilityData Data { get; set; }
        public SuperMegaWTFSpeedAbility()
        {
            Data = Resources.Load<AbilityData>("AbilityData/TestSpeedUp");
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