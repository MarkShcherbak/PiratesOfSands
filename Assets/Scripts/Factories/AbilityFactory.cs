using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class AbilityFactory
    {
        /// <summary>
        /// возвращает рандомную способность первого уровня
        /// </summary>
        /// <returns></returns>
        public static IAbility CreateRandomAbility()
        {
            IAbility ability = null;
            switch (Random.Range(3, 4)) //TODO включить все
            {
                case 0: ability = CreateShootingAbility(); break;
                case 1: ability = CreateSpeedBoostAbility(); break;
                case 2: ability = CreateShieldAbility(); break;
                case 3: ability = CreateHazardAbility(); break;
                default: ability = CreateShootingAbility(); break;
            }
            return ability;
        }

        /// <summary>
        /// возвращает стрелковую способность первого уровня
        /// </summary>
        /// <returns></returns>
        public static IAbility CreateShootingAbility()
        {
            return new CannonballShotAbility();
        }

        /// <summary>
        /// возвращает способность ускорения первого уровня
        /// </summary>
        /// <returns></returns>
        public static IAbility CreateSpeedBoostAbility()
        {
            return new SpeedSmallBoostAbility();
        }

        /// <summary>
        /// возвращает способность щита первого уровня
        /// </summary>
        /// <returns></returns>
        public static IAbility CreateShieldAbility()
        {
            return new ShieldTierOneAbility();
        }

        /// <summary>
        /// возвращает опасную способность первого уровня
        /// </summary>
        /// <returns></returns>
        public static IAbility CreateHazardAbility()
        {
            return new OilAbility();
        }
    }
