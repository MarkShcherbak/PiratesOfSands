using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс трек системы чекпоинтов
/// </summary>
public class TrackPath : MonoBehaviour
{
    public List<Transform> trackPoints;
    public bool isLooped = false;

    private bool isActive = false;

    private void Awake()
    {
        if (trackPoints.Count > 1)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }

    /// <summary>
    /// Визуальное отображение для редкатора сцены
    /// </summary>
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position

        
        if (trackPoints.Count>0)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(trackPoints[0].position, 0.5f);
        }
        Gizmos.color = Color.green;
        for (int i = 1; i < trackPoints.Count; i++)
        {
           
            if (i == trackPoints.Count-1 && !isLooped)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(trackPoints[i].position, 0.5f);
            }
            else
            {
                Gizmos.DrawSphere(trackPoints[i].position, 0.3f);
            }
            

            Gizmos.DrawLine(trackPoints[i - 1].position, trackPoints[i].position);
            

        }

        if (isLooped && trackPoints.Count > 1 )
        {
            Gizmos.DrawLine(trackPoints[trackPoints.Count - 1].position, trackPoints[0].position);
        }
    }

    /// <summary>
    /// Метод возвращает стартовую позицию трека
    /// </summary>
    /// <returns></returns>
    public Transform GetStartPosition()
    {
        if (isActive)
        {
            return trackPoints[0];
        }
        else
        {
            return default;
        }
    }

    /// <summary>
    /// Метод возвращает ближайший чекпоинт трассы
    /// </summary>
    /// <param name="objectPosition">Позиция объекта для определения ближайшей точки</param>
    /// <returns></returns>
    public Transform GetNearCheckPointPosition(Transform objectPosition)
    {
        if (isActive)
        {
            List<KeyValuePair<Transform, float>> flotDistList = new List<KeyValuePair<Transform, float>>();

            for (int i = 0; i < trackPoints.Count; i++)
            {
                flotDistList.Add(new KeyValuePair<Transform, float>(trackPoints[i], Vector3.Distance(objectPosition.position, trackPoints[i].position)));
            }
            flotDistList.Sort((x, y) => (y.Value.CompareTo(x.Value)));
            return flotDistList[flotDistList.Count - 1].Key;

        }
        else
        {
            return default;
        }
        
    }

    /// <summary>
    /// Метод возвращает позицию следующей контрольной точки
    /// </summary>
    /// <param name="lastCheckPoint"> последняя пройденная контрольная точка</param>
    /// <returns></returns>
    private Transform GetNextPosition(Transform lastCheckPoint)
    {
        if (isActive)
        {
            int findInd = trackPoints.FindIndex(x => x == lastCheckPoint);
            if (findInd < trackPoints.Count-1)
            {
                return trackPoints[findInd + 1];
            }
            else if (findInd == trackPoints.Count - 1 && isLooped)
            {
                return trackPoints[0];
            }
            else
            {
                return trackPoints[findInd];
            }
        }
        else
        {
            return default;
        }
    }

    /// <summary>
    /// Метод возвращает следующий чекпоинт к которому надо двигаться
    /// </summary>
    /// <param name="gObj">Трансформ объекта который следует треку</param>
    /// <returns></returns>
    public Transform GetNextCheckPointPosition(Transform gObj)
    {
        Transform curPoint = TrackPositionData.GetCurrentPointToObject(gObj);
        if (isActive && curPoint)
        {
            Transform newPoint = GetNextPosition(curPoint);
            TrackPositionData.SetCurrentPointToObject(gObj, newPoint);
            return newPoint;
        }
        else
        {
            return default;
        }
    }

    /// <summary>
    /// Добавляет объект на трек, для слежения за его прохождением
    /// </summary>
    /// <param name="gObj">объект для слежения</param>
    /// <param name="setNearestPoint">вместо стартовой позиции следующей точкой укаазать ближашую</param>
    public void SetObjPosition(Transform gObj, bool setStartPoint = false)
    {
        if (setStartPoint)
        {
            TrackPositionData.SetCurrentPointToObject(gObj, GetStartPosition());
        }
        else
        {
            TrackPositionData.SetCurrentPointToObject(gObj, GetNearCheckPointPosition(gObj));
        }
    }

}
