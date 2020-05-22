using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineModelView : Singleton<CinemachineModelView>
{
    [SerializeField] private List<CinemachineVirtualCamera> virtCameras;
    [SerializeField] private CinemachineBrain cinemachineBrain;
    public Transform targetPosition = null;

    private int currCam = 0;

    private void Start()
    {
        if (virtCameras.Count==0 || !targetPosition)
        {
            return;
        }

        foreach (CinemachineVirtualCamera cam in virtCameras)
        {
            cam.Follow = targetPosition;
            cam.LookAt = targetPosition;
        }

        SetCurrentCamera();

    }

    public void NextCam()
    {
        currCam++;
        if (currCam> virtCameras.Count-1)
        {
            currCam = 0;
        }
        SetCurrentCamera(currCam);


    }

    private void SetCurrentCamera(int camNum = 0)
    {
        for (int i = 0; i < virtCameras.Count; i++)
        {

            if (i == camNum)
            {
                virtCameras[i].Priority = 1;
            }
            else
            {
                virtCameras[i].Priority = 0;
            }
        }
    }


}
