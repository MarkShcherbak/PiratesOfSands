using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFactory
{
    public static CannonModelView CreateCannonModelView(Transform slot, WeaponData type)
    {
        // Получаем GO пушки
        GameObject cannonPrefab = Resources.Load<GameObject>("Prefabs/Cannons/DefaultCannon");

        // Создаем инстанс и получаем модель-представление пушки
        CannonModelView modelView = UnityEngine.Object.Instantiate(cannonPrefab, slot.transform).GetComponent<CannonModelView>();

        // Назначаем указанный тип снарядов для пушки
        modelView.cannonType = type;

        return modelView;
    }
}
