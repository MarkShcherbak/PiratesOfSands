    using UnityEngine;

    public class GrapplingHookAbility : IAbility
    {
        
        public Color AbilityColor { get; set; }
        
        public GrapplingHookAbility()
        {
            AbilityColor = Color.red;
        }
        public IAbility Add(IAbility ability)
        {
            if (ability is CannonballShotAbility) return new ChainShotAbility();
            else return ability;
        }

        public void Execute(Transform position)
        {
            throw new System.NotImplementedException();
        }
        
    }
