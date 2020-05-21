using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineModelView : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> virtCameras;
    [SerializeField] private CinemachineBrain cinemachineBrain;
    public Transform targetPosition = null;

    private void Start()
    {
        if (virtCameras.Count==0 || !targetPosition)
        {
            return;
        }

        foreach (CinemachineVirtualCamera camera in virtCameras)
        {
            camera.Follow = targetPosition;
            camera.LookAt = targetPosition;
        }

    }


}
