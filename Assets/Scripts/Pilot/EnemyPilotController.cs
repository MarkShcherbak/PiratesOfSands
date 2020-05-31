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

        pilotModelView.ChechpointTarget = currentAim.position;
        
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

            pilotModelView.ChechpointTarget = aimOffset;

            // Получаем множитель скорости движения к следующей цели
            aimInterest = UnityEngine.Random.Range(0.4f, 1f);
        }
    }


    private void HandleActionInput(object sender, Vector3 direction)
    { //TODO сделать обработку действий противника
        shipModelView.ActionInput(direction);
    }

    RaycastHit hit;
    private void HandleMovingInput(object sender, EventArgs e)
    {
        // AI obstacles avoiding (nikita)
        
        float maxDistance = 100f;
        RaycastHit hit;
        bool isLeftHit = false;
        bool isRightHit = false;
        bool isObstacleOnMyWay = false;
        
        //вводим слои для обнаружения земли и для обнаружения подбираемых\уклоняемых сущностей на трассе
        LayerMask TrackEntityMask = LayerMask.GetMask("AICastedEntity");
        LayerMask groundMask = LayerMask.GetMask("Ground");
        
        if (pilotModelView.ChechpointTarget != null)
        {
            // нормализуем вектор направления до чекпоинта, отсекаем ему Y составляющую
            Vector3 checkpointDirection = new Vector3((pilotModelView.ChechpointTarget - pilotModelView.transform.position).normalized.x, 0,
                (pilotModelView.ChechpointTarget - pilotModelView.transform.position).normalized.z) * 2f;
            Vector3 rightDirection = new Vector3(pilotModelView.transform.right.x, 0, pilotModelView.transform.right.z) * 10;
            Vector3 leftDirection = new Vector3(-pilotModelView.transform.right.x, 0, -pilotModelView.transform.right.z) * 10;

            //forward cast
            bool isLandCast = Physics.Raycast(pilotModelView.transform.position + (checkpointDirection + Vector3.up).normalized * 20,
                Vector3.down, out hit, maxDistance, groundMask);
            if (isLandCast)
            {
                float pilotToCastPointDistance = Vector3.Distance(pilotModelView.transform.position, hit.point);
                isObstacleOnMyWay = Physics.BoxCast(pilotModelView.transform.position, pilotModelView.transform.lossyScale / 2, hit.point, out hit,
                    Quaternion.identity, pilotToCastPointDistance, TrackEntityMask);
                if (isObstacleOnMyWay)
                {
                    if (hit.collider.tag.Equals("SlowPoint"))
                    {
                        isRightHit = Physics.BoxCast(pilotModelView.transform.position, pilotModelView.transform.lossyScale / 2, (hit.point + rightDirection).normalized ,
                            Quaternion.identity, pilotToCastPointDistance, TrackEntityMask);
                        
                        isLeftHit = Physics.BoxCast(pilotModelView.transform.position, pilotModelView.transform.lossyScale / 2, (hit.point + leftDirection).normalized ,
                            Quaternion.identity, pilotToCastPointDistance, TrackEntityMask);
                        
                    }
                }
            }

            float moveH;
            
            if (isObstacleOnMyWay)
            {
                if(isRightHit == false)
                {
                    moveH = Vector3.SignedAngle(shipModelView.transform.forward,
                        shipModelView.transform.forward + rightDirection, Vector3.up);
                    Debug.Log("TURNING RIGHT!");
                }

                 else if (isLeftHit == false)
                {
                    moveH = Vector3.SignedAngle(shipModelView.transform.forward,
                        shipModelView.transform.forward + leftDirection, Vector3.up);
                    Debug.Log("TURNING LEFT!");
                }
                else
                {
                    moveH = Vector3.SignedAngle(shipModelView.transform.forward,
                        shipModelView.transform.forward + leftDirection * 4, Vector3.up);
                    Debug.Log("PANIC!");
                }
            }
            else 
                moveH = Vector3.SignedAngle(shipModelView.transform.forward,
                aimOffset - shipModelView.transform.position, Vector3.up);

            Vector3 direction = new Vector3(moveH / 30, 0, aimInterest);
            shipModelView.SteeringInput(direction);

            Debug.DrawLine(shipModelView.transform.position, aimOffset);
        }
    }
}