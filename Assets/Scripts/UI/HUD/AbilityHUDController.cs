using System;
using UnityEngine;

public class AbilityHUDController
{
    private readonly AbilityHUDModelView hudMV;
    private readonly ShipModelView shipMV;

    private Sprite defaultSprite;

    public AbilityHUDController(AbilityHUDModelView modelView, ShipModelView ship)
    {
        hudMV = modelView;
        defaultSprite = hudMV.MasterSlot.sprite;
        shipMV = ship;
        shipMV.OnAbilityChanged += HandneAbilityChanged;

        hudMV.AddContainers(shipMV.LeftCannons, shipMV.RightCannons, shipMV.FrontCannons, shipMV.BackCannons);
    }

    private void HandneAbilityChanged(object sender, Sprite e)
    {
        Sprite sample;

        if (shipMV.LeftCannons[0].LoadedAbility != null)
            sample = shipMV.LeftCannons[0].LoadedAbility.Data.Icon;
        else sample = defaultSprite;
        foreach (var image in hudMV.LeftImages) image.sprite = sample;


        if (shipMV.RightCannons[0].LoadedAbility != null)
            sample = shipMV.RightCannons[0].LoadedAbility.Data.Icon;
        else sample = defaultSprite;
        foreach (var image in hudMV.RightImages) image.sprite = sample;


        if (shipMV.FrontCannons[0].LoadedAbility != null)
            sample = shipMV.FrontCannons[0].LoadedAbility.Data.Icon;
        else sample = defaultSprite;
        foreach (var image in hudMV.ForwardImages) image.sprite = sample;


        if (shipMV.BackCannons[0].LoadedAbility != null)
            sample = shipMV.BackCannons[0].LoadedAbility.Data.Icon;
        else sample = defaultSprite;
        foreach (var image in hudMV.BackImages) image.sprite = sample;


        if (e != null)
            hudMV.MasterSlot.sprite = e;
        else hudMV.MasterSlot.sprite = defaultSprite;
    }
}
