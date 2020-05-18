    using UnityEngine;

    public class CameraFactory
    {
        public static CameraModelView CreateCameraRig(Transform playerTransform)
        {
            // Получаем GO рига
            GameObject cameraRigPrefab = Resources.Load<GameObject>("Prefabs/Camera/CameraRig");

            // Создаем инстанс и получаем модель-представление рига
            CameraModelView modelView = UnityEngine.Object.Instantiate(cameraRigPrefab).GetComponent<CameraModelView>();

            modelView.targetPosition = playerTransform;

            return modelView;
        }
    }
