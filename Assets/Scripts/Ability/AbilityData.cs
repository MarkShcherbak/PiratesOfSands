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
    [SerializeField] private Sprite icon;
    public Sprite Icon { get => icon; set => icon = value; }

    //Материал контейнера на сцене
    [SerializeField] private GameObject containerMesh;
    public GameObject ContainerMesh { get => containerMesh; set => containerMesh = value; }

    // Префаб способности
    [SerializeField] private GameObject prefab;
    public GameObject Prefab { get => prefab; set => prefab = value; }

    // Куда может быть загружена способность
    //[SerializeField] private bool equippableFront, equippableBack, equippableLeft, equippableRight;
    //public bool EquippableFront { get => equippableFront; set => equippableFront = value; }
    //public bool EquippableBack { get => equippableBack; set => equippableBack = value; }
    //public bool EquippableLeft { get => equippableLeft; set => equippableLeft = value; }
    //public bool EquippableRight { get => equippableRight; set => equippableRight = value; }

    #endregion

    // Уникальные параметры для стреляющих способностей
    #region Shooting ability properties

    // Урон от снаряда
    [SerializeField] private float projectileDamage;
    public float ProjectileDamage { get => projectileDamage; set => projectileDamage = value; }

    // Начальная скорость движения снаряда
    [SerializeField] private float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }

    // Продолжительность существования ловушки
    [SerializeField] private float projectileLifetime;
    public float ProjectileLifetime { get => projectileLifetime; set => projectileLifetime = value; }

    // Разброс снарядов
    [SerializeField] private float projectileScatter;
    public float ProjectileScatter { get => projectileScatter; set => projectileScatter = value; }

    // Количество снарядов
    [SerializeField] private float projectilesCount;
    public float ProjectilesCount { get => projectilesCount; set => projectilesCount = value; }

    [SerializeField] private bool projectileIsExplosive;
    public bool ProjectileIsExplosive { get => projectileIsExplosive; set => projectileIsExplosive = value; }

    // Сила взрыва
    [SerializeField] private float projectileExplosionForce;
    public float ProjectileExplosionForce { get => projectileExplosionForce; set => projectileExplosionForce = value; }

    // Радиус взрыва
    [SerializeField] private float projectileExplosionRadius;
    public float ProjectileExplosionRadius { get => projectileExplosionRadius; set => projectileExplosionRadius = value; }

    #endregion

    // Уникальные параметры для ловушек
    #region Hazard ability properties

    // Урон от ловушки
    [SerializeField] private float hazardDamage;
    public float HazardDamage { get => hazardDamage; set => hazardDamage = value; }

    // Скорость полета объекта ловушки
    [SerializeField] private float hazardSpeed;
    public float HazardSpeed { get => hazardSpeed; set => hazardSpeed = value; }

    // Продолжительность существования ловушки
    [SerializeField] private float hazardLifetime;
    public float HazardLifetime { get => hazardLifetime; set => hazardLifetime = value; }

    // Разброс снарядов
    [SerializeField] private float hazardScatter;
    public float HazardScatter { get => hazardScatter; set => hazardScatter = value; }

    // Количество снарядов
    [SerializeField] private float hazardsCount;
    public float HazardsCount { get => hazardsCount; set => hazardsCount = value; }

    // Ловушка взрывается?
    [SerializeField] private bool hazardIsExplosive;
    public bool HazardIsExplosive { get => hazardIsExplosive; set => hazardIsExplosive = value; }

    // Сила взрыва
    [SerializeField] private float hazardExplosionForce;
    public float HazardExplosionForce { get => hazardExplosionForce; set => hazardExplosionForce = value; }

    // Радиус взрыва
    [SerializeField] private float hazardExplosionRadius;
    public float HazardExplosionRadius { get => hazardExplosionRadius; set => hazardExplosionRadius = value; }

    #endregion

    // Уникальные параметры для щитов
    #region Shield ability properties

    // Длительность работы щита
    [SerializeField] private float shieldDuration;
    public float ShieldDuration { get => shieldDuration; set => shieldDuration = value; }

    // Долговечность щита
    [SerializeField] private float shieldDurability;
    public float ShieldDurability { get => shieldDurability; set => shieldDurability = value; }

    #endregion

    // Уникальные параметры для бустов
    #region SpeedUp ability properties

    // Продолжительность работы буста
    [SerializeField] private float speedUpDuration;
    public float SpeedUpDuration { get => speedUpDuration; set => speedUpDuration = value; }

    // Эффект от буста
    [SerializeField] private float speedUpIntensity;
    public float SpeedUpIntensity { get => speedUpIntensity; set => speedUpIntensity = value; }

    // Максимальная скорость буста
    [SerializeField] private float speedUpMaxSpeed;
    public float SpeedUpMaxSpeed { get => speedUpMaxSpeed; set => speedUpMaxSpeed = value; }

    #endregion
}

[CustomEditor(typeof(AbilityData))]
public class AbilityDataEditor : Editor
{
    private AbilityData Data { get { return (AbilityData)target; } }

    override public void OnInspectorGUI()
    {
        // Основные параметры SO - иконка, префаб и задержка перед применением
        Data.Icon = (Sprite)EditorGUILayout.ObjectField("Icon", Data.Icon, typeof(Sprite), false);
        Data.Prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", Data.Prefab, typeof(GameObject), false);

        // Параметры, указывающие, в какой слот возможна загрузка способности
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        //EditorGUILayout.PrefixLabel("Possible slots");

        //EditorGUILayout.BeginHorizontal();
        //Data.EquippableFront = EditorGUILayout.ToggleLeft("Front", Data.EquippableFront, GUILayout.MaxWidth(100));
        //Data.EquippableLeft = EditorGUILayout.ToggleLeft("Left", Data.EquippableLeft, GUILayout.MaxWidth(100));
        //EditorGUILayout.EndHorizontal();

        //EditorGUILayout.BeginHorizontal();
        //Data.EquippableBack = EditorGUILayout.ToggleLeft("Back", Data.EquippableBack, GUILayout.MaxWidth(100));
        //Data.EquippableRight = EditorGUILayout.ToggleLeft("Right", Data.EquippableRight, GUILayout.MaxWidth(100));
        //EditorGUILayout.EndHorizontal();

        //EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        // Тип способности
        Data.AbilityType = (AbilityData.Ability)EditorGUILayout.EnumPopup("Ability type", Data.AbilityType);

        // Вывод возможных параметров в зависимости от выбранного типа способности
        switch (Data.AbilityType)
        {
            case AbilityData.Ability.Shooting:

                Data.ContainerMesh = Resources.Load<GameObject>("Prefabs/AbilityPickups/AbilityProjectile");
                Data.ProjectileDamage = EditorGUILayout.FloatField("Damage", Data.ProjectileDamage);
                Data.ProjectileSpeed = EditorGUILayout.FloatField("Speed", Data.ProjectileSpeed);
                Data.ProjectileLifetime = EditorGUILayout.FloatField("Lifetime", Data.ProjectileLifetime);
                Data.ProjectileScatter = EditorGUILayout.FloatField("Scatter", Data.ProjectileScatter);
                Data.ProjectilesCount = EditorGUILayout.FloatField("Projectiles count", Data.ProjectilesCount);

                Data.ProjectileIsExplosive = EditorGUILayout.BeginToggleGroup("Explosive?", Data.ProjectileIsExplosive);
                    Data.ProjectileExplosionForce = EditorGUILayout.FloatField("Force", Data.ProjectileExplosionForce);
                    Data.ProjectileExplosionRadius = EditorGUILayout.FloatField("Radius", Data.ProjectileExplosionRadius);
                EditorGUILayout.EndToggleGroup();

                EditorUtility.SetDirty(target);
                break;

            case AbilityData.Ability.Hazard:

                Data.ContainerMesh = Resources.Load<GameObject>("Prefabs/AbilityPickups/AbilityHazard");
                Data.HazardDamage = EditorGUILayout.FloatField("Damage", Data.HazardDamage);
                Data.HazardSpeed = EditorGUILayout.FloatField("Speed", Data.HazardSpeed);
                Data.HazardLifetime = EditorGUILayout.FloatField("Lifetime", Data.HazardLifetime);
                Data.HazardScatter = EditorGUILayout.FloatField("Scatter", Data.HazardScatter);
                Data.HazardsCount = EditorGUILayout.FloatField("Hazards count", Data.HazardsCount);

                Data.HazardIsExplosive = EditorGUILayout.BeginToggleGroup("Explosive?", Data.HazardIsExplosive);
                    Data.HazardExplosionForce = EditorGUILayout.FloatField("Force", Data.HazardExplosionForce);
                    Data.HazardExplosionRadius = EditorGUILayout.FloatField("Radius", Data.HazardExplosionRadius);
                EditorGUILayout.EndToggleGroup();

                EditorUtility.SetDirty(target);
                break;

            case AbilityData.Ability.Shield:

                Data.ContainerMesh = Resources.Load<GameObject>("Prefabs/AbilityPickups/AbilityShield");
                Data.ShieldDuration = EditorGUILayout.FloatField("Duration", Data.ShieldDuration);
                Data.ShieldDurability = EditorGUILayout.FloatField("Durability", Data.ShieldDurability);
                EditorUtility.SetDirty(target);
                break;

            case AbilityData.Ability.SpeedUp:

                Data.ContainerMesh = Resources.Load<GameObject>("Prefabs/AbilityPickups/AbilitySpeedUp");
                Data.SpeedUpDuration = EditorGUILayout.FloatField("Duration", Data.SpeedUpDuration);
                Data.SpeedUpIntensity = EditorGUILayout.FloatField("Intensity", Data.SpeedUpIntensity);
                Data.SpeedUpMaxSpeed = EditorGUILayout.FloatField("Max speed", Data.SpeedUpMaxSpeed);
                EditorUtility.SetDirty(target);
                break;

            default:
                break;
        }
    }
}