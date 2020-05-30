using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EnemyPilotModelView : MonoBehaviour
{
    public event EventHandler OnMovingInput = (sender, e) => { };
    public event EventHandler<Vector3> OnActionInput = (sender, e) => { };
    public event EventHandler<Transform> OnTriggerCollision = (sender, checkPointTransform) => { };

    private float moveH, moveV;
    private Vector3 actionDirection;
    
    public Vector3 ChechpointTarget { get; set; }
    
    //TODO добавить ИИ для управления и действий

    private void FixedUpdate()
    {
        OnMovingInput(this, EventArgs.Empty);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerCollision(this, other.transform);
    }

    #region Gizmos

    

   
    void OnDrawGizmos()
    {
        float maxDistance = 100f;
        RaycastHit hit;

        if (ChechpointTarget != null)
        {

            Vector3 forwardDirection = new Vector3((ChechpointTarget - transform.position).normalized.x, 0,
                (ChechpointTarget - transform.position).normalized.z) * 2f;
            Vector3 rightDirection = new Vector3(transform.right.x, 0, transform.right.z) * 0.75f;

            //forward cast
            bool isHit = Physics.BoxCast(transform.position + (forwardDirection + Vector3.up).normalized * 20,
                transform.lossyScale / 2, Vector3.down, out hit,
                transform.rotation, maxDistance);
            if (isHit)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(hit.point, transform.lossyScale);
                Gizmos.DrawLine(transform.position,
                    transform.position + (forwardDirection + transform.up).normalized * 20);
                Gizmos.DrawLine(transform.position + (forwardDirection + transform.up).normalized * 20, hit.point);
            }

            //right cast
            isHit = Physics.BoxCast(
                transform.position + (forwardDirection + Vector3.up + rightDirection).normalized * 20,
                transform.lossyScale / 2, Vector3.down, out hit,
                transform.rotation, maxDistance);
            if (isHit)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(hit.point, transform.lossyScale);
                Gizmos.DrawLine(transform.position,
                    transform.position + (forwardDirection + Vector3.up + rightDirection).normalized * 20);
                Gizmos.DrawLine(transform.position + (forwardDirection + Vector3.up + rightDirection).normalized * 20,
                    hit.point);
            }

            //left cast
            isHit = Physics.BoxCast(
                transform.position + (forwardDirection + Vector3.up + -rightDirection).normalized * 20,
                transform.lossyScale / 2, Vector3.down, out hit,
                transform.rotation, maxDistance);
            if (isHit)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(hit.point, transform.lossyScale);
                Gizmos.DrawLine(transform.position,
                    transform.position + (forwardDirection + Vector3.up + -rightDirection).normalized * 20);
                Gizmos.DrawLine(transform.position + (forwardDirection + Vector3.up + -rightDirection).normalized * 20,
                    hit.point);
            }
        }
    }
    
    #endregion
}
