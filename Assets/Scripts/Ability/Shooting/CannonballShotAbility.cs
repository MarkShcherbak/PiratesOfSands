    using UnityEngine;

    public class CannonballShotAbility : IAbility
    {
        public Color AbilityColor { get; set; }
        public AbilityData Data { get; set; }
        
        public CannonballShotAbility()
        {
            Data = Resources.Load<AbilityData>("AbilityData/TestCannon");
        }

        public IAbility Add(IAbility ability)
        {
            if (ability is CannonballShotAbility) return new GrapplingHookAbility();
            else return ability;
        }

        public void Execute(Transform position)
        {
        Debug.Log("Cannonball fired!");
        }
    }
