using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Data;
using System;

//[Serializable]
/// <summary>
/// Класс трек системы чекпоинтов
/// </summary>
public class TrackPath : MonoBehaviour
{
    [SerializeField] public List<Transform> trackPoints;
    public bool isLooped = false;
    public int countOfLaps = 1;

    private bool isActive = false;

    private List<KeyValuePair<Transform, float>> checkPointAndDistance;

    /// <summary>
    /// Список точек, в которых есть ворота, через них идёт регистрация прохождения
    /// </summary>
    [SerializeField] public List<Transform> gatePoints;

    /// <summary>
    /// Список точек, в которых есть ворота выстроенный по порядку для прохождения, через них идёт регистрация прохождения
    /// </summary>
    private List<Transform> gatePointsFollow;

    /// <summary>
    /// соответствие пилота и его текущей видимой точки, ворот
    /// </summary>
    private Dictionary<Transform, int> pilotsAndCurrentGatePoint;

    /// <summary>
    /// Ключ - номер чекпоинта с воротами из списка gatePointsFollow и структура для фиксации прохождения
    /// </summary>
    private List<KeyValuePair<int, TrackLeaderTableStruct>> trackLeaderBoard;

    /// <summary>
    /// Таблица содержит ключ значение для определения финишировал ли пилот
    /// </summary>
    private Dictionary<Transform, bool> hasPilotFinished;

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

        pilotsAndCurrentGatePoint = new Dictionary<Transform, int>();
        trackLeaderBoard = new List<KeyValuePair<int, TrackLeaderTableStruct>>();
        hasPilotFinished = new Dictionary<Transform, bool>();

        SetCheckpointDistance();
        MakeGateFollow();

  

    }

 

    /// <summary>
    /// Метод устанавливает связь чекпоинт - дистанция от начала прохождения в list checkPointAndDistance
    /// </summary>
    private void SetCheckpointDistance()
    {
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
    /// Класс выстраивает последовательность ворот для прохождения
    /// берёт все ворота из gatePoints
    /// порядок ворот выстраивает из trackPoints
    /// </summary>
    private void MakeGateFollow()
    {

        gatePointsFollow = new List<Transform>();
        for (int i = 1; i <= countOfLaps; i++)
        {
            foreach (Transform trackPoint in trackPoints)
            {
                if (gatePoints.Contains(trackPoint))
                {
                    gatePointsFollow.Add(trackPoint);
                }
            }
            if (!isLooped)
            {
                break;
            }
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
    /// и регистрирует в текущем чекпоинте как в пройденном
    /// в случае если чекпоинт является видимым (воротами), 
    /// </summary>
    /// <param name="pilot">Трансформ объекта который следует треку</param>
    /// <returns></returns>
    public Transform GetNextCheckPointAndCheckIn(Transform pilot, Transform trigerTransform)
    {
        Transform curPoint = TrackPositionData.GetCurrentPointToObject(pilot);

        if (isActive && curPoint)
        {
            if (curPoint == trigerTransform)
            {
                Transform newPoint = GetNextPosition(curPoint);
                TrackPositionData.SetCurrentPointToObject(pilot, newPoint);
                TrySetPilotNextGatePointAndCheckInTable(pilot, curPoint);

                return newPoint;
            }
            else
            {
                return curPoint;
            }
            
        }
        else
        {
            return default;
        }
    }

    /// <summary>
    /// проверяет входит ли текущий чекпоинт в состав gatepoint
    /// если входит, то смещает следующий чекпоинт для пилота
    /// и делает запись в таблицу результатов
    /// </summary>
    /// <param name="pilot"></param>
    /// <param name="trackPoint"></param>
    private void TrySetPilotNextGatePointAndCheckInTable(Transform pilot, Transform trackPoint)
    {

        if (gatePoints.Contains(trackPoint))
        {
            int nextGatePointID = pilotsAndCurrentGatePoint[pilot]+1;

            
            if ((nextGatePointID >= gatePointsFollow.Count && isLooped)||
                (!isLooped&& nextGatePointID >= gatePointsFollow.Count-1) )
            {
                
                AddRowToLeaderTable(pilot, nextGatePointID, true);
                
            }else if (trackPoint == gatePointsFollow[nextGatePointID])
            {
                pilotsAndCurrentGatePoint[pilot] = nextGatePointID;
                AddRowToLeaderTable(pilot, nextGatePointID);
                
            }
            
        }
        
    }

    /// <summary>
    /// Регистрирует пилота в системи учет видимых чекпоинтов и определяет начало старта гонки
    /// </summary>
    /// <param name="pilot"></param>
    private void StartRegisterPilotInGate(Transform pilot)
    {
        if (gatePointsFollow.Count==0)
        {
            return;
        }

        AddRowToLeaderTable(pilot, 0);


        pilotsAndCurrentGatePoint.Add(pilot, 0);
        hasPilotFinished.Add(pilot, false);
    }

    /// <summary>
    /// Добавляет строку в словарь рейтинга лидеров прохождения гонки
    /// </summary>
    /// <param name="pilot"></param>
    /// <param name="gateNumber"></param>
    private void AddRowToLeaderTable(Transform pilot,  int gateNumber, bool isFinish = false)
    {
        if (hasPilotFinished.ContainsKey(pilot) && hasPilotFinished[pilot])
        {
            GetFinishTrackLeaderBoard();
            return;
           
        }

        if (isFinish)
        {
            hasPilotFinished[pilot] = true;
        }

        TrackLeaderTableStruct trackLeaderTableStruct = new TrackLeaderTableStruct
        {
            pilot = pilot,
            gateNumber = gateNumber,
            dateTime = System.DateTime.Now,
            isFinish = isFinish
            
        };

        if (gateNumber< gatePointsFollow.Count)
        {
            trackLeaderTableStruct.gateTransform = gatePointsFollow[gateNumber];
        }
        
        //trackLeaderBoard.Add(gateNumber, trackLeaderTableStruct);
        trackLeaderBoard.Add(new KeyValuePair<int, TrackLeaderTableStruct>(gateNumber, trackLeaderTableStruct));
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
    /// <param name="pilotTransform">объект для слежения</param>
    /// <param name="setNearestPoint">вместо стартовой позиции следующей точкой укаазать ближашую</param>
    public void SetObjPosition(Transform pilotTransform, bool setStartPoint = false)
    {
        if (setStartPoint)
        {
            Transform startPosition = GetStartPosition();
            TrackPositionData.SetCurrentPointToObject(pilotTransform, startPosition);
            StartRegisterPilotInGate(pilotTransform);
        }
        else
        {
            TrackPositionData.SetCurrentPointToObject(pilotTransform, GetNearCheckPointPosition(pilotTransform));
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

   /// <summary>
   /// Возвращает список "строк" кораблей прошедших финишную черту и время прохождения 
   /// </summary>
   /// <returns></returns>
    public List<string> GetFinishTrackLeaderBoard()
    {
        List<KeyValuePair<int, TrackLeaderTableStruct>> LODFadesdaMode = trackLeaderBoard;

        List<KeyValuePair<string, DateTime>> leaderList = new List<KeyValuePair<string, DateTime>>();
        foreach (KeyValuePair<int, TrackLeaderTableStruct> item in trackLeaderBoard)
        {
            if (item.Key == gatePointsFollow.Count)
            {
                leaderList.Add(new KeyValuePair<string, DateTime>(item.Value.pilot.name, item.Value.dateTime));
            }
        }
        leaderList.OrderBy(o => o.Value);
        List<string> leaderListString = new List<string>();
        foreach (KeyValuePair<string, DateTime> item in leaderList)
        {
            leaderListString.Add(item.Key +"; " + item.Value.ToString());
        }
        
        return leaderListString;
    }

    /// <summary>
    /// Возвращает трансформ следующей по порядку точки с воротами,  если следующей точки нет, возвращает первую.
    /// </summary>
    /// <param name="pilot"></param>
    /// <returns></returns>
    public Transform GetNextGatePoint(Transform pilot)
    {
        if (gatePointsFollow.Count == 0)
        {
            return default;
        }
        
        int numberGate;
        if (pilotsAndCurrentGatePoint.ContainsKey(pilot))
        {
            numberGate = pilotsAndCurrentGatePoint[pilot];
        }
        else
        {
            numberGate = 0;
        }
        numberGate++;
        if (gatePointsFollow.Count> numberGate)
        {
            return gatePointsFollow[numberGate];
        }
        else
        {
            return gatePointsFollow[0];
        }
    }
}
