using System;
using UnityEngine;

public class PlayerPilotController
{
    private readonly PlayerPilotModelView playerModelView;
    private readonly ShipModelView shipModelView;
    private readonly TrackPath checkpointsPath;




    public PlayerPilotController(PlayerPilotModelView player, ShipModelView ship, TrackPath checkpoints)
    {
        playerModelView = player;
        shipModelView = ship;
        checkpointsPath = checkpoints;

        checkpointsPath.SetObjPosition(shipModelView.transform, ship, true, true);


        playerModelView.OnMovingInput += HandleMovingInput;
        InputControl.Instance.OnActionInput += HandleActionInput;

        playerModelView.OnTriggerCollision += HandleTriggerCollision;
    }

    private void HandleActionInput(object sender, Vector3 direction)
    {
        shipModelView.ActionInput(direction);
    }

    private void HandleMovingInput(object sender, Vector3 direction)
    {
        shipModelView.SteeringInput(direction);
    }

    private void HandleTriggerCollision(object sender, Transform checkpointTransform)
    {
        if (checkpointTransform.tag.Equals("TrackPoint"))
        {
            checkpointsPath.GetNextCheckPointAndCheckIn(shipModelView.transform, checkpointTransform); 
        }
    }
}
