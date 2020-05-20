using System;
using UnityEngine;

//interface for cannon
public interface ICannonModelView
{
    IAbility LoadedAbility { get; set; }

    void Fire();

}
