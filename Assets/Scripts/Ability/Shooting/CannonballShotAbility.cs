    using UnityEngine;

    public class CannonballShotAbility : IAbility
    {
        public Color AbilityColor { get; set; }
        
        public CannonballShotAbility()
        {
            AbilityColor = Color.red;
        }

        public IAbility Add(IAbility ability)
        {
            if (ability is CannonballShotAbility) return new GrapplingHookAbility();
            else return ability;
        }

        public void Execute(Transform position)
        {
            //TODO AmmoFactory.CreateCannonballShot(position);
        }
    }
