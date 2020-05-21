    using UnityEngine;

    public class BombAbility : IAbility
    {
        public AbilityData Data { get; set; }

        public BombAbility()
        {
            Data = Resources.Load<AbilityData>("AbilityData/TestHazard");
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
