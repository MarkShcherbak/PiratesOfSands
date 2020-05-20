    using UnityEngine;

    public class BombAbility : IAbility
    {
        public Color AbilityColor { get;set;}

        public BombAbility()
        {
            AbilityColor = Color.yellow;
        }

        public IAbility Add(IAbility ability)
        {
            if (ability is OilAbility) return new NapalmAbility();
            else return ability;
        }

        public void Execute(Transform position)
        {
            Debug.Log("Bomb launched!");
        }
    }
