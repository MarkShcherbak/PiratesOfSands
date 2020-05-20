    using UnityEngine;

    public class GatilngShotAbility : IAbility
    {
        
        public Color AbilityColor { get; set; }
        
        public GatilngShotAbility()
        {
            AbilityColor = Color.red;
        }
        public IAbility Add(IAbility ability)
        {
            if (ability is CannonballShotAbility) return this;
            else return ability;
        }

        public void Execute(Transform position)
        {
            throw new System.NotImplementedException();
        }
    }
