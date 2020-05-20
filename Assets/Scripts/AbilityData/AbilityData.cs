using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AbilityData", order = 1)]

[CustomEditor(typeof(AbilityData))]
public class AbilityDataEditor : Editor
{
    override public void OnInspectorGUI()
    {
        // Получаем SO для выдачи данных в инспекторе
        var data = target as AbilityData;

        // Основные параметры SO - иконка, префаб и задержка перед применением
        data.Icon = EditorGUILayout.ObjectField("Icon", data.Icon, typeof(Sprite), false);
        data.Prefab = EditorGUILayout.ObjectField("Prefab", data.Prefab, typeof(GameObject), false);
        data.Delay = EditorGUILayout.FloatField("Delay before using", data.Delay);

        // Параметры, указывающие, в какой слот возможна загрузка способности
        EditorGUILayout.Separator();

            EditorGUILayout.PrefixLabel("Possible slots");

            EditorGUILayout.BeginHorizontal();
                data.EquippableFront = EditorGUILayout.ToggleLeft("Front", data.EquippableFront, GUILayout.MaxWidth(100));
                data.EquippableLeft = EditorGUILayout.ToggleLeft("Left", data.EquippableLeft, GUILayout.MaxWidth(100));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
                data.EquippableBack = EditorGUILayout.ToggleLeft("Back", data.EquippableBack, GUILayout.MaxWidth(100));
                data.EquippableRight = EditorGUILayout.ToggleLeft("Right", data.EquippableRight, GUILayout.MaxWidth(100));
            EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();

        // Тип способности
        data.AbilityType = (AbilityData.Ability)EditorGUILayout.EnumPopup("Ability type", data.AbilityType);

        // Вывод возможных параметров в зависимости от выбранного типа способности
        switch (data.AbilityType)
        {
            case AbilityData.Ability.Shooting:

                data.ProjectileDamage = EditorGUILayout.FloatField("Damage", data.ProjectileDamage);
                data.ProjectileSpeed = EditorGUILayout.FloatField("Speed", data.ProjectileSpeed);
                data.ProjectileScatter = EditorGUILayout.FloatField("Scatter", data.ProjectileScatter);

                break;

            case AbilityData.Ability.Hazard:

                data.HazardDamage = EditorGUILayout.FloatField("Damage", data.HazardDamage);

                break;

            case AbilityData.Ability.Shield:

                data.ShieldDuration = EditorGUILayout.FloatField("Duration", data.ShieldDuration);

                break;

            case AbilityData.Ability.SpeedUp:

                data.SpeedUpDuration = EditorGUILayout.FloatField("Duration", data.SpeedUpDuration);
                data.SpeedUpIntensity = EditorGUILayout.FloatField("Intensity", data.SpeedUpIntensity);

                break;

            default:
                break;
        }
    }
}

public class AbilityData : ScriptableObject
{
    // Тип способности
    private Ability abilityType;
    public Ability AbilityType { get => abilityType; set => abilityType = value; }

    public enum Ability
    {
        Shooting,
        Hazard,
        Shield,
        SpeedUp
    }

    // Общие параметры способностей
    #region General properties

    // Иконка способности
    private UnityEngine.Object icon;
    public UnityEngine.Object Icon { get => icon; set => icon = value; }

    // Префаб способности
    private UnityEngine.Object prefab;
    public UnityEngine.Object Prefab { get => prefab; set => prefab = value; }

    // Задержка перед применением способности
    private float delay;
    public float Delay { get => delay; set => delay = value; }

    // Куда может быть загружена способность
    private bool equippableFront, equippableBack, equippableLeft, equippableRight;
    public bool EquippableFront { get => equippableFront; set => equippableFront = value; }
    public bool EquippableBack { get => equippableBack; set => equippableBack = value; }
    public bool EquippableLeft { get => equippableLeft; set => equippableLeft = value; }
    public bool EquippableRight { get => equippableRight; set => equippableRight = value; }

    #endregion

    // Уникальные параметры для стреляющих способностей
    #region Shooting ability properties

    // Урон от снаряда
    private float projectileDamage;
    public float ProjectileDamage { get => projectileDamage; set => projectileDamage = value; }

    // Начальная скорость движения снаряда
    private float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }

    // Разброс снарядов
    private float projectileScatter;
    public float ProjectileScatter { get => projectileScatter; set => projectileScatter = value; }

    #endregion

    // Уникальные параметры для размещаемых способностей
    #region Hazard ability properties

    private float hazardDamage;
    public float HazardDamage { get => hazardDamage; set => hazardDamage = value; }

        // etc.

    #endregion

    // Уникальные параметры для щитов
    #region Shield ability properties

    // Длительность работы щита
    private float shieldDuration;
    public float ShieldDuration { get => shieldDuration; set => shieldDuration = value; }

        // etc.

    #endregion

    // Уникальные параметры для бустов
    #region SpeedUp ability properties

    // Продолжительность работы буста
    private float speedUpDuration;
    public float SpeedUpDuration { get => speedUpDuration; set => speedUpDuration = value; }

    // Эффект от буста
    private float speedUpIntensity;
    public float SpeedUpIntensity { get => speedUpIntensity; set => speedUpIntensity = value; }

        // etc.

    #endregion
}