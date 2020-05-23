using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEditor;
using System;

//Вспомогательный класс для создания трека и его чекпоинтов
public class TrackEditor : EditorWindow
{
    [MenuItem("Window/Track editor")]
    public static void Init()
    {
        var window = EditorWindow.GetWindow<TrackEditor>("Track editor");
        DontDestroyOnLoad(window);
        
    }

    private readonly string nameTrackPathPref = "TrackSystem/TrackPathPrefab";
    private readonly string nameTrackPointPref = "TrackSystem/TrackPointPrefab";
    private readonly string nameGatePointPref = "TrackSystem/GatePointPrefab";
    private readonly string shortNameGatePointPref = "GatePointPrefab";
    private GameObject selectedGameObject;

    Editor editor;
    
    private void OnGUI()
    {
        if (!editor) { editor = Editor.CreateEditor(this); }
        if (editor) { editor.OnInspectorGUI(); }
        
        GUILayout.Space(10);

        if (selectedGameObject)
        {
            GUILayout.Label("Selected track: " + selectedGameObject.name.ToString());
        }

        GUILayout.Space(10);

        bool buttonClickedAddTrack = GUILayout.Button("Add track");
        if (buttonClickedAddTrack)
        {
            AddTrack();
        }

        GUILayout.Space(20);

        bool buttonClickedAddPoint = GUILayout.Button("Add point");
        if (buttonClickedAddPoint)
        {
            AddPoint();
        }
        bool buttonClickedDelPoint = GUILayout.Button("Del point");
        if (buttonClickedDelPoint)
        {
            DelPoint();
        }



        GUILayout.Space(20);

        bool buttonClickedMakeGate = GUILayout.Button("Make points a gate");
        if (buttonClickedMakeGate)
        {
            MakeGate();
        }

        bool buttonClickedDelGate = GUILayout.Button("Del gate from points");
        if (buttonClickedDelGate)
        {
            DelGate();
        }


        GUILayout.Space(20);

        bool buttonClickedPlaceOnGround = GUILayout.Button("Place object on ground");
        if (buttonClickedPlaceOnGround)
        {
            PlaceObjectOnGroundButton();
        }

        GUILayout.Space(50);

        bool buttonClickedClearNulls = GUILayout.Button("Clear null points");
        if (buttonClickedClearNulls)
        {
            ClearNullPPoints();
        }
        

        //GUILayout.Space(10);
        //bool buttonClickedAddWave = GUILayout.Button("Add wave");
        //if (buttonClickedAddWave)
        //{
        //    GenerateWave();
        //}

        //bool buttonClickedSaveMeshWave = GUILayout.Button("Save Mesh Wave");
        //if (buttonClickedAddWave)
        //{
        //    SaveMeshWave();
        //}

        //GUILayout.Space(10);
        //bool buttonClickedLvlGenerate = GUILayout.Button("Generate lvl");
        //if (buttonClickedLvlGenerate)
        //{
        //    generateLvl();
        //}


    }

    private void DelPoint()
    {
        TrackPath trp = selectedGameObject.GetComponent<TrackPath>();

        foreach (Transform selectedTrans in Selection.transforms)
        {
            if (selectedTrans.gameObject.GetComponent<TrackPoint>())
            {
                trp.gatePoints.Remove(selectedTrans);
                trp.trackPoints.Remove(selectedTrans);
                DestroyImmediate(selectedTrans.gameObject);
            }
        }
        ClearNullPPoints();
    }

    private void DelGate()
    {
        TrackPath trp = selectedGameObject.GetComponent<TrackPath>();

        foreach (Transform selectedTrans in Selection.transforms)
        {
            if (selectedTrans.gameObject.GetComponent<TrackPoint>())
            {
                Transform objToDel = selectedTrans.Find(shortNameGatePointPref);
                if (objToDel)
                {
                    trp.gatePoints.Remove(selectedTrans);
                    DestroyImmediate(objToDel.gameObject);
                }
            }
        }
        ClearNullPPoints();
    }

    private void PlaceObjectOnGroundButton()
    {
        foreach (Transform selectedTrans in Selection.transforms)
        {
            PlaceObjectOnGround(selectedTrans);
        }
        ClearNullPPoints();
    }

    private void MakeGate()
    {
        var prefab = Resources.Load(nameGatePointPref);
        TrackPath trp = selectedGameObject.GetComponent<TrackPath>();

        foreach (Transform transformPoint in Selection.transforms)
        {
            Transform childGate = transformPoint.Find(shortNameGatePointPref);

            if (transformPoint.gameObject.GetComponent<TrackPoint>() && !childGate)
            {
                GameObject newObj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                newObj.transform.parent = transformPoint;
                newObj.transform.position = transformPoint.position;
                PlaceObjectOnGround(newObj.transform);
                newObj.transform.rotation = transformPoint.rotation;
                trp.gatePoints.Add(transformPoint);
            }
        }
        ClearNullPPoints();

    }

    private void PlaceObjectOnGround(Transform transformToPlace)
    {
        Vector3 posForUFO = transformToPlace.position;
        posForUFO.y = 200f;
        RaycastHit raycastHit = new RaycastHit();
        bool wasTouch = Physics.Raycast(posForUFO, Vector3.down, out raycastHit);
        if (wasTouch)
        {
            transformToPlace.position = raycastHit.point;
        }
    }

    private void OnSelectionChange()
    {
        if (!Selection.activeGameObject)
        {
            return;
        }
        TrackPath tPath = Selection.activeGameObject.GetComponent<TrackPath>();
        if (tPath)
        {
            selectedGameObject = Selection.activeGameObject;
            Repaint();
        }
    }

    private void AddPoint(Vector3 posToCreate = new Vector3())
    {
        
        if (selectedGameObject)
        {
            TrackPath trp = selectedGameObject.GetComponent<TrackPath>();
            
            var prefab = Resources.Load(nameTrackPointPref);
            GameObject newObj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
           
            newObj.transform.parent = selectedGameObject.transform;
            int countName = trp.trackPoints.Count + 1;
            newObj.name = "TrackPoint" + countName.ToString();
            if (trp.trackPoints.Count> 0)
            {
                Vector3 newPos = trp.trackPoints[trp.trackPoints.Count - 1].position;
                newPos.x+=1f;

                newObj.transform.position = newPos;
            }
            else
            {
                newObj.transform.position = posToCreate;
            }
            PlaceObjectOnGround(newObj.transform);

            trp.trackPoints.Add(newObj.transform);

            Selection.activeGameObject = newObj;
        }

        ClearNullPPoints();

    }

    private void ClearNullPPoints()
    {
        TrackPath trp = selectedGameObject.GetComponent<TrackPath>();

        trp.trackPoints.RemoveAll(item => item == null);
        trp.gatePoints.RemoveAll(item => item == default);


    }

    private void AddTrack()
    {

        var prefab = Resources.Load(nameTrackPathPref);
        selectedGameObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;


        selectedGameObject.name = "NewTrack";
        AddPoint();
        AddPoint();
    }


    //Работа с генератором поверхности.

    private Waves waves;

    private void GenerateWave()
    {
        waves.PreGenerateMesh();
    }

    private void SaveMeshWave()
    {
        AssetDatabase.CreateAsset(waves.GetComponent<MeshFilter>().sharedMesh, "meshes");
        AssetDatabase.SaveAssets();
    }

    private LevelGeneration lvelgenerator;

    private void generateLvl()
    {
        lvelgenerator.GenerateMap();
    }
}
