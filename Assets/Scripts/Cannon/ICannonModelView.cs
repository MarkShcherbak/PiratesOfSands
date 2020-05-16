using System;
using UnityEngine;

//interface for cannon
public interface ICannonModelView
{
    IAbilityModelView LoadedAbility { get; set; }

    void Fire();

}
