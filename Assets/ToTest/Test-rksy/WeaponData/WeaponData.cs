using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    // Тип оружия

    public Type type = Type.Round;
    public enum Type
    {
        Round,
        Chain,
        Grape,
        Special
    }

    // Начальное количество патронов
    public int ammo;

    // Скорость стрельбы
    public float fireRate;

    // Префаб снаряда
    public GameObject projectile;

    // Скорость движения снаряда
    public float projectileSpeed;

    // Количество выпускаемых снарядов за один выстрел
    public int projectilesAtOnce;

    // Разброс снарядов
    public float projectileScatter;
}
