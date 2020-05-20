using System;
using UnityEngine;

//interface for Ability
public interface IAbility
{
    Color AbilityColor { get; set; }
    IAbility Add(IAbility ability);

    void Execute(Transform position);
}
