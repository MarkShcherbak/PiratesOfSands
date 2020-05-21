using System;
using System.Collections.Generic;
using Track;
using UnityEngine;

// класс игры, создает игру
public class GameController
{
    public event EventHandler OnDestroyGame = (sender, e) => { };
    
    private TrackModelView trackMV;  // ссылка на трек trackModelView;
    private Canvas canvas;
    private PauseMenuModelView pauseMV = null; // ссылка на UI окошко паузы
    private List<GameObject> objectsInGame; // лист объектов (нужен для удаления при выходе в меню)
    private Camera mainCamera;
    
    
        public GameController(Canvas mainCanvas, Camera mainCam) // конструктор игры, можно сделать несколько конструкторов(например сколько противников, какая сложность, какая трасса)
        {
            mainCamera = mainCam;
            objectsInGame = new List<GameObject>();
            canvas = mainCanvas;
            trackMV =  TrackFactory.CreateTestTrackModelView(); // создаем трассу и добавляем в лист объектов в игре
            trackMV.OnPause += HandleGamePause; // подписываем обработчик паузы на событие паузы
            objectsInGame.Add(trackMV.gameObject);
            
            
        //создаем сеть чекпоинтов
        TrackPath checkpointsPath = TrackFactory.CreateTestTrackPath();
        objectsInGame.Add(checkpointsPath.gameObject);
            
        // создаем корабль игрока
        ShipModelView playerShipMV = ShipFactory.CreateShipModelView(checkpointsPath.trackPoints[0].position + new Vector3(2,2,2)); //TODO сделать разброс появления на старте!
        ShipController shipController = ShipFactory.CreateShipController(playerShipMV);
        objectsInGame.Add(playerShipMV.gameObject);

        // создаем пилота игрока
        PlayerPilotModelView playerPilotMV = PilotFactory.CreatePlayerPilotModelView(playerShipMV.transform);
        PlayerPilotController playerController = PilotFactory.CreatePlayerPilotController(playerPilotMV, playerShipMV);
        objectsInGame.Add(playerPilotMV.gameObject);

        // TODO создаем HUD отображение способностей (ТЕСТОВОЕ!!!)
        AbilityHUDModelView abilityHUDMV = UIFactory.CreatePlayerAbilityUI(canvas);
        objectsInGame.Add(abilityHUDMV.gameObject);
        AbilityHUDController abilityHUDController = new AbilityHUDController(abilityHUDMV, playerShipMV);
          
        // создаем риг камер
        CameraModelView cameraMV = CameraFactory.CreateCameraRig(playerShipMV.transform);
        mainCamera.gameObject.SetActive(false);  // отключаем основную камеру после появления рига
        objectsInGame.Add(cameraMV.gameObject);
        
        // создаем корабль противника
        ShipModelView enemyShipMV = ShipFactory.CreateShipModelView(checkpointsPath.trackPoints[0].position + new Vector3(-10,2,-2));
        ShipController enemyShipController = ShipFactory.CreateShipController(enemyShipMV);
        objectsInGame.Add(enemyShipMV.gameObject);
        
        // создаем пилота противника
        EnemyPilotModelView enemyPilotMV = PilotFactory.CreateEnemyPilotModelView(enemyShipMV.transform);
        EnemyPilotController enemyPilotController =
            PilotFactory.CreateEnemyPilotController(enemyPilotMV, enemyShipMV, checkpointsPath);
        objectsInGame.Add(enemyPilotMV.gameObject);
        





        // TODO создаем AI и корабли AI и передаем им сеть чекпоинтов
        }

        
        private void HandleGamePause(object sender, EventArgs e) // обрабатываем нажатие Escape во время игры (в апдейте трека - пока прикрутил туда)
        {
            if (Time.timeScale != 0.0f)
            {
                pauseMV = UIFactory.CreatePauseMenuModelView(canvas); // создаем менюшку паузы
                Time.timeScale = 0.0f;

                pauseMV.OnResume += HandleResumeGame; // подписываем обработчик продолжения игры на событие OnResume
                pauseMV.OnExitToMainMenu += HandleExitToMenu;
            }
            else HandleResumeGame(null, EventArgs.Empty); // позволяет отключить меню паузы, нажав esc еще раз
            
        }

        private void HandleExitToMenu(object sender, EventArgs e)
        {
            mainCamera.gameObject.SetActive(true);
            DestroyGame();
            Time.timeScale = 1;
        }

        private void HandleResumeGame(object sender, EventArgs e)
        {
            MonoBehaviour.Destroy(pauseMV.gameObject); // удаляем менюшку паузы
            Time.timeScale = 1;
        }

        public void DestroyGame()
        {
            if(pauseMV != null) MonoBehaviour.Destroy(pauseMV.gameObject); // удаляем менюшку паузы если есть
            foreach (GameObject obj in objectsInGame) // удаляем все GameObject, которые создавали во время игры
            {
                MonoBehaviour.Destroy(obj);
            }
            OnDestroyGame(this, EventArgs.Empty); // вызываем событие, на которое подписан Main
        }
    }
