    using UnityEngine;

    public class SpeedBigBoostAbility : IAbility
    {
        
        public Color AbilityColor { get; set; }
        
        public SpeedBigBoostAbility()
        {
            AbilityColor = Color.green;
        }
        public IAbility Add(IAbility ability)
        {
            if (ability is SpeedSmallBoostAbility) return new SpeedFlyingBoostAbility();
            else return ability;
        }

        public void Execute(Transform position)
        {
        Debug.Log("Big Speed boost launched!");
        }
    }
