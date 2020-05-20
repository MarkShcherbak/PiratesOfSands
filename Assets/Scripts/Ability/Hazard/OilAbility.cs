    using UnityEngine;

    public class OilAbility : IAbility
    {
        public Color AbilityColor { get; set;}
        
        public OilAbility()
        {
            AbilityColor = Color.yellow;
        }

        public IAbility Add(IAbility ability)
        {
            if (ability is OilAbility) return new BombAbility();
            else return ability;
        }

        public void Execute(Transform position)
        {
            Debug.Log("Oil launched!");
        }
    }
