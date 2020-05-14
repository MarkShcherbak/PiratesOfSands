using System;
using UnityEngine;

// фабрика трасс, создает заранее подготовленные трассы из папки Resources, либо процедурно сгенерированные
    public class TrackFactory
    {
        public static TrackModelView CreateTestTrackModelView()
        {
            GameObject testTrackPrefab = Resources.Load<GameObject>("Prefabs/Track/TestTrack");
            TrackModelView modelView = UnityEngine.Object.Instantiate(testTrackPrefab)
                .GetComponent<TrackModelView>();
            return modelView;
        }

        
        // генерация процедруной трассы к примеру через такой интерфейс
        // TODO для Марка
        public static TrackModelView CreateProceduralTrackModelView(int checkPointCount, int pickUpsCount)
        {
           throw new Exception("тут пока пусто");
        }
    }