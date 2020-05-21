using System;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyPilotController
{
    private readonly EnemyPilotModelView pilotModelView;
    private readonly ShipModelView shipModelView;
    private readonly TrackPath checkpointsPath;

    private Transform currentAim;

    public EnemyPilotController(EnemyPilotModelView enemyPilot, ShipModelView ship, TrackPath checkpoints)
    {
        pilotModelView = enemyPilot;
        shipModelView = ship;
        checkpointsPath = checkpoints;

        
        currentAim = checkpointsPath.GetStartPosition();
        checkpointsPath.SetObjPosition(pilotModelView.transform, true);
        
        pilotModelView.OnMovingInput += HandleMovingInput;
        pilotModelView.OnActionInput += HandleActionInput;
        pilotModelView.OnTriggerCollision += HandleTriggerCollision;

    }

    private void HandleTriggerCollision(object sender, string tag)
    {
        if (tag.Equals("TrackPoint"))
        {
            currentAim = checkpointsPath.GetNextCheckPointPositionForGameObject(pilotModelView.transform); //?????
        }
    }


    private void HandleActionInput(object sender, Vector3 direction)
    { //TODO сделать обработку действий противника
        shipModelView.ActionInput(direction);
    }

    private void HandleMovingInput(object sender, EventArgs e)
    {
        if (currentAim != null)
        {
            float moveH = Vector3.SignedAngle(shipModelView.transform.forward,
                currentAim.position - shipModelView.transform.position, Vector3.up);

            Vector3 direction = new Vector3(moveH / 360, 0, 0.1f);
            shipModelView.SteeringInput(direction.normalized);
        }
    }
}