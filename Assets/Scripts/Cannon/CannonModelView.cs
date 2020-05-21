using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonModelView : MonoBehaviour, ICannonModelView
{
    [SerializeField] private Transform shotOrigin;

    public IAbility LoadedAbility { get; set; }

    public void Fire()
    {
       LoadedAbility.Execute(shotOrigin);
    }
}