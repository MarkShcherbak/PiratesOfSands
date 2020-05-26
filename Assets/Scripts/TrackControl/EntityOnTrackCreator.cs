using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Класс создает точки по треку, в которых будут генерироваться бусты и прочие активности.
/// </summary>
public class EntityOnTrackCreator : MonoBehaviour
{
    private TrackPath trackPath;
    /// <summary>
    /// Количество точек, в которых будут генерироваться бусты
    /// </summary>
    [SerializeField] private int countPointsToCreate;
    /// <summary>
    /// Максимальный радиус, в котором создается точка на случайном расстоянии от трека
    /// </summary>
    [SerializeField] private float maxDistFromPath;
    /// <summary>
    /// префаб точки, которая будет генерировать бусты
    /// </summary>
    [SerializeField] private GameObject pointCreator;

    /// <summary>
    /// Связка позиции для создания и созданного объекта. 
    /// </summary>
    private Dictionary<GameObject, GameObject> pointsAndCreations;

    /// <summary>
    /// Создавать в точках генерации указанные префабы
    /// </summary>
    [SerializeField] private bool generateConstantly = true;

    /// <summary>
    /// префабы которые будут создаваться в точках генерации
    /// </summary>
    [SerializeField] private List<GameObject> objsToCreate;

    [SerializeField] private float timeToCorutineCreator = 5f;



    private void Start()
    {
        trackPath = GetComponent<TrackPath>();
        pointsAndCreations = new Dictionary<GameObject, GameObject>();
        float entitieDistance = trackPath.GetDistanceFromStart(trackPath.trackPoints[trackPath.trackPoints.Count-1]) / (countPointsToCreate+1);
        float currentDistance = 0f;

        for (int i = 0; i < countPointsToCreate; i++)
        {
            currentDistance += entitieDistance;

            KeyValuePair<Transform, float>  cKVP = trackPath.GetChekpointThrouDistance(currentDistance);

            
            Vector3 curPoint = cKVP.Key.position;
            Vector3 nextPoint = trackPath.GetNextCheckPointPosition(cKVP.Key).position;
            float m1 = currentDistance - cKVP.Value;
            float m2 = Vector3.Distance(curPoint, nextPoint)- m1;

            float x = (m2 * curPoint.x + m1 * nextPoint.x) / (m1 + m2);
            float y = (m2 * curPoint.y + m1 * nextPoint.y) / (m1 + m2);
            float z = (m2 * curPoint.z + m1 * nextPoint.z) / (m1 + m2);

            float xrnd = Random.Range(-maxDistFromPath, maxDistFromPath);
            float zrnd = Random.Range(-maxDistFromPath, maxDistFromPath);

            Vector3 posToCreate = new Vector3(x+ xrnd, y+100f,z+ zrnd);

            RaycastHit raycastHit = new RaycastHit();
            bool wasTouch = Physics.Raycast(posToCreate, Vector3.down, out raycastHit);
            if (wasTouch)
            {
                pointsAndCreations.Add(Instantiate(pointCreator, raycastHit.point, Quaternion.identity, gameObject.transform), default);
            }

        }
        CheckAndCreateEntities();

        if (generateConstantly && objsToCreate.Count>0)
        {
            StartCoroutine(CreateEntitiesCorut());
        }


    }

    IEnumerator CreateEntitiesCorut()
    {
        while (generateConstantly)
        {
            yield return new WaitForSecondsRealtime(timeToCorutineCreator);
            CheckAndCreateEntities();
        }
        
    }

    /// <summary>
    /// Метод проверяет и создает активности в точках генерации
    /// </summary>
    private void CheckAndCreateEntities()
    {
        List<GameObject> listToCreate = new List<GameObject>();
        foreach (KeyValuePair<GameObject, GameObject> point in pointsAndCreations)
        {
            if (point.Value == default)
            {
                listToCreate.Add(point.Key);
                
            }
        }
        foreach (GameObject item in listToCreate)
        {
            GameObject newEntity = Instantiate(objsToCreate[Random.Range(0, objsToCreate.Count)], item.transform.position, Quaternion.identity, gameObject.transform);
            EntitiyDeleter entitiyDeleter = newEntity.AddComponent<EntitiyDeleter>();
            entitiyDeleter.SetParentCreator(this);

            pointsAndCreations[item] = newEntity;
        }
            
            
    }

    /// <summary>
    /// Удаляет объект из списка существующих, после этого может создваться новый буст
    /// </summary>
    /// <param name="objToRemove">объект префаб</param>
    public void RemoveObjectFromSlot(GameObject objToRemove)
    {

        if (pointsAndCreations.ContainsValue(objToRemove))
        {
            GameObject myKey = pointsAndCreations.FirstOrDefault(x => x.Value == objToRemove).Key;
            pointsAndCreations[myKey] = default;
        }
        
    }

}
