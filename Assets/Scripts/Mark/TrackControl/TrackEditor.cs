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

        GUILayout.Space(10);

        bool buttonClickedAddPoint = GUILayout.Button("Add point");
        if (buttonClickedAddPoint)
        {
            AddPoint();
        }

        GUILayout.Space(10);
        bool buttonClickedAddWave = GUILayout.Button("Add wave");
        if (buttonClickedAddWave)
        {
            GenerateWave();
        }
        
        bool buttonClickedSaveMeshWave = GUILayout.Button("Save Mesh Wave");
        if (buttonClickedAddWave)
        {
            SaveMeshWave();
        }

        GUILayout.Space(10);
        bool buttonClickedLvlGenerate = GUILayout.Button("Generate lvl");
        if (buttonClickedLvlGenerate)
        {
            generateLvl();
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

            selectedGameObject.GetComponent<TrackPath>().trackPoints.Add(newObj.transform);

            Selection.activeGameObject = newObj;
        }
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

    public Waves waves;

    private void GenerateWave()
    {
        waves.PreGenerateMesh();
    }

    private void SaveMeshWave()
    {
        AssetDatabase.CreateAsset(waves.GetComponent<MeshFilter>().sharedMesh, "meshes");
        AssetDatabase.SaveAssets();
    }

    public LevelGeneration lvelgenerator;

    private void generateLvl()
    {
        lvelgenerator.GenerateMap();
    }
}
