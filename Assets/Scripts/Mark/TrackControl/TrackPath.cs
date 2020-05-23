using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Класс трек системы чекпоинтов
/// </summary>
public class TrackPath : MonoBehaviour
{
    public List<Transform> trackPoints;
    public bool isLooped = false;

    private bool isActive = false;

    private List<KeyValuePair<Transform, float>> checkPointAndDistance;

    public List<Transform> gatePoints;

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
        checkPointAndDistance = new List<KeyValuePair<Transform, float>>();

        checkPointAndDistance.Add(new KeyValuePair<Transform, float>(trackPoints[0], 0f));

        float trackDist = 0f;
        for (int i = 1; i < trackPoints.Count; i++)
        {
            trackDist += Vector3.Distance(trackPoints[i - 1].position, trackPoints[i].position);

            checkPointAndDistance.Add(new KeyValuePair<Transform, float>(trackPoints[i], trackDist));
        }

    }


    /// <summary>
    /// Визуальное отображение для редкатора сцены
    /// </summary>
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position

        Color color;
        if (trackPoints.Count > 0)
        {
            color = Color.blue;
            color.a = 0.2f;
            Gizmos.color = color;
            Gizmos.DrawSphere(trackPoints[0].position, 20f);
        }
        
        color = Color.green;
        color.a = 0.2f;
        Gizmos.color = color;
        for (int i = 1; i < trackPoints.Count; i++)
        {
            if (!trackPoints[i]|| !trackPoints[i-1])
            {
                continue;
            }

            if (i == trackPoints.Count - 1 && !isLooped)
            {
                color = Color.red;
                color.a = 0.2f;
                Gizmos.color = color;
                Gizmos.DrawSphere(trackPoints[i].position, 20f);
            }
            else
            {
                Gizmos.DrawSphere(trackPoints[i].position, 20f);
            }


            Gizmos.DrawLine(trackPoints[i - 1].position, trackPoints[i].position);

        }

        if (isLooped && trackPoints.Count > 1)
        {
            if (trackPoints[trackPoints.Count - 1] && trackPoints[0])
            {
                Gizmos.DrawLine(trackPoints[trackPoints.Count - 1].position, trackPoints[0].position);
            }
           
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
            if (findInd < trackPoints.Count - 1)
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
    /// Метод возвращает следующий чекпоинт к которому надо двигаться для указанного объекта
    /// </summary>
    /// <param name="gObj">Трансформ объекта который следует треку</param>
    /// <returns></returns>
    public Transform GetNextCheckPointPositionForGameObject(Transform gObj)
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
    ///  Метод возвращает следующий чекпоинт относительно указанного
    ///  если чекпоинт последний возвращает его же
    /// </summary>
    /// <param name="gObj"></param>
    /// <returns></returns>
    public Transform GetNextCheckPointPosition(Transform curCheckPoint)
    {
        int curIndex = trackPoints.IndexOf(curCheckPoint) + 1;

        if (curIndex < (trackPoints.Count - 1))
        {
            return trackPoints[curIndex];
        }
        else
        {
            return trackPoints[trackPoints.Count - 1];
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

    /// <summary>
    /// Возрващеает чекпоинта точки со старта
    /// </summary>
    /// <param name="curPoint"></param>
    /// <returns></returns>
    public float GetDistanceFromStart(Transform curPoint)
    {
        KeyValuePair<Transform, float> rv = checkPointAndDistance.FirstOrDefault(d => d.Key == curPoint);
        return rv.Value;


    }

    /// <summary>
    /// Возвращает пару - чекпоинт, который последним вошёл в указанную дистанцию с начала трассы
    /// и расстояние от начала трассы
    /// </summary>
    /// <param name="dist"></param>
    /// <returns></returns>
    public KeyValuePair<Transform, float> GetChekpointThrouDistance(float dist)
    {
        List<KeyValuePair<Transform, float>> ll = checkPointAndDistance.Where(d => d.Value <= dist).ToList();

        if (ll.Count > 0)
        {
            return ll[ll.Count - 1];
        }
        else
        {
            return default;
        }

    }

}
