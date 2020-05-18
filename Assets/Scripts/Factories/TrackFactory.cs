using System;
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
    }