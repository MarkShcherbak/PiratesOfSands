using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyPilotController
{
    private readonly EnemyPilotModelView pilotModelView;
    private readonly ShipModelView shipModelView;
    private readonly TrackPath checkpointsPath;

    private Transform currentAim;

    // Точка финальной цели для пилота корабля с уже добавленным разбросом
    private Vector3 aimOffset;

    // "Шум" скорости передвижения
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

            // Получаем компонент коллайдера, описывающего расстояние между столбами ворот
            BoxCollider collider = currentAim.GetComponentInChildren<BoxCollider>();

            // Получаем случайную точку исходя из размеров коллайдера
            aimOffset = new Vector3(
                x: UnityEngine.Random.Range(collider.bounds.min.x, collider.bounds.max.x), 
                y: collider.bounds.max.y, 
                z: UnityEngine.Random.Range(collider.bounds.min.z, collider.bounds.max.z));

            // и размещаем эту точку внутри коллайдера (т.е. возвращаем ближайшую соответствующую полученным координатам точку в пространстве коллайдера)
            aimOffset = collider.ClosestPoint(aimOffset);

            // Получаем множитель скорости движения к следующей цели
            aimInterest = UnityEngine.Random.Range(0.4f, 1f);
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
                aimOffset - shipModelView.transform.position, Vector3.up);

            Vector3 direction = new Vector3(moveH / 30, 0, aimInterest);
            shipModelView.SteeringInput(direction);

            Debug.DrawLine(shipModelView.transform.position, aimOffset);
        }
    }
}
