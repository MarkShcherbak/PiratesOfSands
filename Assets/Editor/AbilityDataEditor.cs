using UnityEngine;
using UnityEditor;

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
