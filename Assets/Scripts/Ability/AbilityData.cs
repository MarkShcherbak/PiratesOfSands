using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AbilityData", order = 1)]

public class AbilityData : ScriptableObject
{
    // Тип способности
    [SerializeField] private Ability abilityType;
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
    [SerializeField] private UnityEngine.Object icon;
    public UnityEngine.Object Icon { get => icon; set => icon = value; }

    // Префаб способности
    [SerializeField] private GameObject prefab;
    public GameObject Prefab { get => prefab; set => prefab = value; }

    // Задержка перед применением способности
    [SerializeField] private float delay;
    public float Delay { get => delay; set => delay = value; }

    // Куда может быть загружена способность
    [SerializeField] private bool equippableFront, equippableBack, equippableLeft, equippableRight;
    public bool EquippableFront { get => equippableFront; set => equippableFront = value; }
    public bool EquippableBack { get => equippableBack; set => equippableBack = value; }
    public bool EquippableLeft { get => equippableLeft; set => equippableLeft = value; }
    public bool EquippableRight { get => equippableRight; set => equippableRight = value; }

    #endregion

    // Уникальные параметры для стреляющих способностей
    #region Shooting ability properties

    // Урон от снаряда
    [SerializeField] private float projectileDamage;
    public float ProjectileDamage { get => projectileDamage; set => projectileDamage = value; }

    // Начальная скорость движения снаряда
    [SerializeField] private float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }

    // Разброс снарядов
    [SerializeField] private float projectileScatter;
    public float ProjectileScatter { get => projectileScatter; set => projectileScatter = value; }

    #endregion

    // Уникальные параметры для размещаемых способностей
    #region Hazard ability properties

    [SerializeField] private float hazardDamage;
    public float HazardDamage { get => hazardDamage; set => hazardDamage = value; }

    // etc.

    #endregion

    // Уникальные параметры для щитов
    #region Shield ability properties

    // Длительность работы щита
    [SerializeField] private float shieldDuration;
    public float ShieldDuration { get => shieldDuration; set => shieldDuration = value; }

    // etc.

    #endregion

    // Уникальные параметры для бустов
    #region SpeedUp ability properties

    // Продолжительность работы буста
    [SerializeField] private float speedUpDuration;
    public float SpeedUpDuration { get => speedUpDuration; set => speedUpDuration = value; }

    // Эффект от буста
    [SerializeField] private float speedUpIntensity;
    public float SpeedUpIntensity { get => speedUpIntensity; set => speedUpIntensity = value; }

    // etc.

    #endregion
}

[CustomEditor(typeof(AbilityData))]
public class AbilityDataEditor : Editor
{
    private AbilityData Data { get { return (AbilityData) target; } }

    override public void OnInspectorGUI()
    {
        // Основные параметры SO - иконка, префаб и задержка перед применением
        Data.Icon = EditorGUILayout.ObjectField("Icon", Data.Icon, typeof(Sprite), false);
        Data.Prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", Data.Prefab, typeof(GameObject), false);
        Data.Delay = EditorGUILayout.FloatField("Delay before using", Data.Delay);

        // Параметры, указывающие, в какой слот возможна загрузка способности
        EditorGUILayout.Separator();

        EditorGUILayout.PrefixLabel("Possible slots");

        EditorGUILayout.BeginHorizontal();
            Data.EquippableFront = EditorGUILayout.ToggleLeft("Front", Data.EquippableFront, GUILayout.MaxWidth(100));
            Data.EquippableLeft = EditorGUILayout.ToggleLeft("Left", Data.EquippableLeft, GUILayout.MaxWidth(100));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
            Data.EquippableBack = EditorGUILayout.ToggleLeft("Back", Data.EquippableBack, GUILayout.MaxWidth(100));
            Data.EquippableRight = EditorGUILayout.ToggleLeft("Right", Data.EquippableRight, GUILayout.MaxWidth(100));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();

        // Тип способности
        Data.AbilityType = (AbilityData.Ability)EditorGUILayout.EnumPopup("Ability type", Data.AbilityType);

        // Вывод возможных параметров в зависимости от выбранного типа способности
        switch (Data.AbilityType)
        {
            case AbilityData.Ability.Shooting:

                Data.ProjectileDamage = EditorGUILayout.FloatField("Damage", Data.ProjectileDamage);
                Data.ProjectileSpeed = EditorGUILayout.FloatField("Speed", Data.ProjectileSpeed);
                Data.ProjectileScatter = EditorGUILayout.FloatField("Scatter", Data.ProjectileScatter);
                EditorUtility.SetDirty(target);
                break;

            case AbilityData.Ability.Hazard:

                Data.HazardDamage = EditorGUILayout.FloatField("Damage", Data.HazardDamage);
                EditorUtility.SetDirty(target);
                break;

            case AbilityData.Ability.Shield:

                Data.ShieldDuration = EditorGUILayout.FloatField("Duration", Data.ShieldDuration);
                EditorUtility.SetDirty(target);
                break;

            case AbilityData.Ability.SpeedUp:

                Data.SpeedUpDuration = EditorGUILayout.FloatField("Duration", Data.SpeedUpDuration);
                Data.SpeedUpIntensity = EditorGUILayout.FloatField("Intensity", Data.SpeedUpIntensity);
                EditorUtility.SetDirty(target);
                break;

            default:
                break;
        }
    }
}