    using UnityEngine;

    public class SpeedSmallBoostAbility : IAbility
    {
        public Color AbilityColor { get; set; }
        
        public SpeedSmallBoostAbility()
        {
            AbilityColor = Color.green;
        }
        public IAbility Add(IAbility ability)
        {
            if (ability is SpeedSmallBoostAbility) return new SpeedBigBoostAbility();
            else return ability;
        }

        public void Execute(Transform position)
        {
        Debug.Log("Small speed Boost launched!");
        }
    }
