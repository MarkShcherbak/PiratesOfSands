    using UnityEngine;

    public class NapalmAbility : IAbility
    {
        public Color AbilityColor { get; set;}
        
        public NapalmAbility()
        {
            AbilityColor = Color.yellow;
        }

        public IAbility Add(IAbility ability)
        {
            if (ability is OilAbility) return new NetAbility();
            else return ability;        
        }
        

        public void Execute(Transform position)
        {
            throw new System.NotImplementedException();
        }
    }
