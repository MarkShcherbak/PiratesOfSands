    using UnityEngine;

    public class ChainShotAbility : IAbility
    {
        public Color AbilityColor { get; set; }
        
        public ChainShotAbility()
        {
            AbilityColor = Color.red;
        }
        public IAbility Add(IAbility ability)
        {
            if (ability is CannonballShotAbility) return new GatilngShotAbility();
            else return ability;
        }

        public void Execute(Transform position)
        {
            throw new System.NotImplementedException();
        }
    }