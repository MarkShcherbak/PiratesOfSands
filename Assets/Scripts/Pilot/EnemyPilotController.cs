﻿using System;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyPilotController
{
    private readonly EnemyPilotModelView pilotModelView;
    private readonly ShipModelView shipModelView;
    private readonly TrackPath checkpointsPath;

    private Transform currentAim;

    // Смещение от центра точки интереса
    private Vector3 aimOffset;

    private float aimInterest = 1.0f;

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

    private void HandleTriggerCollision(object sender, Transform checkpointTransform)
    {
        if (checkpointTransform.tag.Equals("TrackPoint"))
        {
            currentAim = checkpointsPath.GetNextCheckPointAndCheckIn(pilotModelView.transform, checkpointTransform); //?????

            // Получаем смещение в зависимости от размера коллайдера в самих воротах
            aimOffset = new Vector3(UnityEngine.Random.Range(- checkpointTransform.GetComponentInChildren<BoxCollider>().size.x, checkpointTransform.GetComponentInChildren<BoxCollider>().size.x) * 0.3f, 0, 0);

            // Получаем множитель скорости движения к следующей цели
            aimInterest = UnityEngine.Random.Range(0.6f, 1f);
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
