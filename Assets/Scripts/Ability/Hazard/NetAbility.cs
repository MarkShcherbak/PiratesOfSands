    using UnityEngine;

    public class NetAbility : IAbility
    {
        public Color AbilityColor { get; set;}
        
        public NetAbility()
        {
            AbilityColor = Color.yellow;
        }

        public IAbility Add(IAbility ability)
        {
            if (ability is OilAbility) return this;
            else return ability;
        }

        public void Execute(Transform position)
        {
            throw new System.NotImplementedException();
        }
    }
