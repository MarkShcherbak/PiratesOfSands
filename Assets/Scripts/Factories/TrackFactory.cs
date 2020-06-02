using System;
using Track;
using UnityEngine;

// фабрика трасс, создает заранее подготовленные трассы из папки Resources, либо процедурно сгенерированные
    public class TrackFactory
    {
        // public static TrackModelView CreateTestTrackModelView()
        // {
        //     GameObject testTrackPrefab = Resources.Load<GameObject>("Prefabs/Track/MarkTestTrack");
        //     TrackModelView modelView = UnityEngine.Object.Instantiate(testTrackPrefab)
        //         .GetComponent<TrackModelView>();
        //     return modelView;
        // }
        //
        // public static TrackPath CreateTestTrackPath()
        // {
        //     GameObject testTrackPathPrefab = Resources.Load<GameObject>("Prefabs/Track/CheckpointsPath");
        //     TrackPath path = UnityEngine.Object.Instantiate(testTrackPathPrefab)
        //         .GetComponent<TrackPath>();
        //     return path;
        // }
        
        public static TrackModelView CreateBigTrackModelView()
        {
            GameObject testTrackPrefab = Resources.Load<GameObject>("Prefabs/Track/desert_race_track");
            TrackModelView modelView = UnityEngine.Object.Instantiate(testTrackPrefab)
                .GetComponent<TrackModelView>();
            return modelView;
        }

        public static TrackPath CreateBigTrackPath()
        {
            GameObject testTrackPathPrefab = Resources.Load<GameObject>("Prefabs/Track/DesertRaceCheckpointsPath");
            TrackPath path = UnityEngine.Object.Instantiate(testTrackPathPrefab)
                .GetComponent<TrackPath>();
            return path;
        }

        public static AbilityContainerModelView CreateAbilityContainer(Vector3 position)
        {
            GameObject containerPrefab = Resources.Load<GameObject>("Prefabs/Track/AbilityContainer");
            AbilityContainerModelView modelView = UnityEngine.Object.Instantiate(containerPrefab,position,Quaternion.identity)
                .GetComponent<AbilityContainerModelView>();
            return modelView;
        }

        public static StartPlacerModelView CreateStartPlacer(Transform startGate)
        {
            GameObject placerPrefab = Resources.Load<GameObject>("Prefabs/Track/StartPlacer");
            StartPlacerModelView modelView = UnityEngine.Object.Instantiate(placerPrefab, startGate.position,startGate.rotation)
                .GetComponent<StartPlacerModelView>();
            return modelView;
        }
    }