using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

    public class CameraModelView : MonoBehaviour
    {
        [SerializeField] private List<Camera> camList;
        [SerializeField] private Transform centerPosition;

        private float mouseX;
        
        public Transform targetPosition = null;

        private int currentCamera;
        private void Start()
        {
            currentCamera = 0;
            ChangeCamera();
        }

        private void ChangeCamera()
        {
            for (int i = 0; i < camList.Count; i++)
            {
                if (i == currentCamera)
                {
                    camList[i].gameObject.SetActive(true);
                }
                else camList[i].gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                currentCamera++;
                if (currentCamera == camList.Count)
                {
                    currentCamera = 0;
                }
                ChangeCamera();
            }

            mouseX = Input.GetAxis("Mouse X");
        }

        private void FixedUpdate()
        {
            if (targetPosition != null)
            {
                if (centerPosition.position != targetPosition.position)
                {
                    centerPosition.position =
                        Vector3.Lerp(centerPosition.position, targetPosition.position, 0.5f);
                }

                if (mouseX != 0f)
                {
                    
                    centerPosition.Rotate(Vector3.up, 20.0f * mouseX);
                }
            }
        }
    }
