using System;
using UnityEngine;

public class PlayerPilotController
{
    private readonly PlayerPilotModelView playerModelView;
    private readonly ShipModelView shipModelView;
   

    public PlayerPilotController(PlayerPilotModelView player, ShipModelView ship)
    {
        playerModelView = player;
        shipModelView = ship;


        playerModelView.OnMovingInput += HandleMovingInput;
        InputControl.Instance.OnActionInput += HandleActionInput;

    }

    private void HandleActionInput(object sender, Vector3 direction)
    {
        shipModelView.ActionInput(direction);
    }

    private void HandleMovingInput(object sender, Vector3 direction)
    {
        shipModelView.SteeringInput(direction);
    }
}
