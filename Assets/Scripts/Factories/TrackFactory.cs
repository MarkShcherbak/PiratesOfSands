using System;
using Track;
using UnityEngine;

// фабрика трасс, создает заранее подготовленные трассы из папки Resources, либо процедурно сгенерированные
    public class TrackFactory
    {
        public static TrackModelView CreateTestTrackModelView()
        {
            GameObject testTrackPrefab = Resources.Load<GameObject>("Prefabs/Track/MarkTestTrack");
            TrackModelView modelView = UnityEngine.Object.Instantiate(testTrackPrefab)
                .GetComponent<TrackModelView>();
            return modelView;
        }

        public static TrackPath CreateTestTrackPath()
        {
            GameObject testTrackPathPrefab = Resources.Load<GameObject>("Prefabs/Track/CheckpointsPath");
            TrackPath path = UnityEngine.Object.Instantiate(testTrackPathPrefab)
                .GetComponent<TrackPath>();
            return path;
        }

        public static AbilityContainerModelView CreateAbilityContainer()
        {
            GameObject containerPrefab = Resources.Load<GameObject>("Prefabs/Track/AbilityContainer");
            AbilityContainerModelView modelView = UnityEngine.Object.Instantiate(containerPrefab)
                .GetComponent<AbilityContainerModelView>();
            return modelView;
        }
    }