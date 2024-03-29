﻿using System;
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
    private TrackFinishMenuModelView trackFinishMenuModel = null; // ссылка на UI окошко паузы
    private List<GameObject> objectsInGame; // лист объектов (нужен для удаления при выходе в меню)
    private Camera mainCamera;
    private TrackPath checkpointsPath;
    private AlertsModelView alertsModelView = null;
    public GameStats gameStats;


    public GameController(Canvas mainCanvas, Camera mainCam, GameStats gameStat, int lapsCount, int shipCount) // конструктор игры, можно сделать несколько конструкторов(например сколько противников, какая сложность, какая трасса)
    {
        mainCamera = mainCam;
        objectsInGame = new List<GameObject>();
        canvas = mainCanvas;
        gameStats = gameStat;

        //создаем трассу
        trackMV =  TrackFactory.CreateBigTrackModelView(gameStats.testTrackPrefab); // создаем трассу и добавляем в лист объектов в игре
        trackMV.OnPause += HandleGamePause; // подписываем обработчик паузы на событие паузы
        objectsInGame.Add(trackMV.gameObject);
        
        //создаем сеть чекпоинтов
        checkpointsPath = TrackFactory.CreateBigTrackPath(gameStats.testTrackPathPrefab, lapsCount);
        checkpointsPath.OnFinish += HandleTrackFinish; 
        objectsInGame.Add(checkpointsPath.gameObject);
            
        // создаем объект размещения кораблей на трассе
        StartPlacerModelView placerMV = TrackFactory.CreateStartPlacer(checkpointsPath.GetStartPosition(), gameStats.placerPrefab);

        // создаем корабль игрока
        ShipModelView playerShipMV = ShipFactory.CreateShipModelView(placerMV.GetSpawnPoint(0));
        ShipController shipController = ShipFactory.CreateShipController(playerShipMV, null);
        playerShipMV.gameObject.AddComponent<AudioListener>();
        objectsInGame.Add(playerShipMV.gameObject);

        playerShipMV.name = "Player";

        ///Создаем эффект песчаной бури и привязываем к игроку 
        EffectModelView effectModelView = EffectsFactory.CreateSandstormEffect(gameStats.sandStormPrefab);
        effectModelView.transform.parent = playerShipMV.transform;
        effectModelView.transform.position = playerShipMV.transform.position;

        // создаем HUD стрелку направления
        DirectionArrowModelView HUDarrowMV = UIFactory.CreateDirectionArrow(canvas);
        
        // создаем пилота игрока
        PlayerPilotModelView playerPilotMV = PilotFactory.CreatePlayerPilotModelView(playerShipMV.transform);
        PlayerPilotController playerController = PilotFactory.CreatePlayerPilotController(playerPilotMV, playerShipMV, checkpointsPath, HUDarrowMV);
        objectsInGame.Add(playerPilotMV.gameObject);
        UIFactory.AddMinimapPointToPlayer(playerPilotMV.transform);


        for (int i = 1; i < shipCount; i++)
        {
            // создаем корабль противника
            ShipModelView enemyShipMV = ShipFactory.CreateShipModelView(placerMV.GetSpawnPoint(i));
            objectsInGame.Add(enemyShipMV.gameObject);

            enemyShipMV.name = $"Enemy {i}";

            // создаем показатель хитпоинтов корабля противника
            HitpointsCanvasModelView enemyHp = UIFactory.CreateShipHealthBar(enemyShipMV.transform);
            if (enemyHp == null) Debug.Log("HP NOT CREATED!!");
            objectsInGame.Add(enemyHp.gameObject);

            // создаем контроллер корабля противника
            ShipController enemyShipController = ShipFactory.CreateShipController(enemyShipMV,enemyHp);

            // создаем пилота противника
            EnemyPilotModelView enemyPilotMV = PilotFactory.CreateEnemyPilotModelView(enemyShipMV.transform);
            EnemyPilotController enemyPilotController =
                PilotFactory.CreateEnemyPilotController(enemyPilotMV, enemyShipMV, checkpointsPath);
            enemyShipMV.enemyPilotController = enemyPilotController;
            objectsInGame.Add(enemyPilotMV.gameObject);

            UIFactory.AddMinimapPointToEnemy(enemyPilotMV.transform);
        }

        // TODO создаем HUD отображение способностей (ТЕСТОВОЕ!!!)
        AbilityHUDModelView abilityHUDMV = UIFactory.CreatePlayerAbilityUI(canvas);
        objectsInGame.Add(abilityHUDMV.gameObject);
        AbilityHUDController abilityHUDController = new AbilityHUDController(abilityHUDMV, playerShipMV);

        // создаем риг камер
        CinemachineModelView cameraMV = CameraFactory.CreateCameraRig(playerShipMV.transform);
        mainCamera.gameObject.SetActive(false);  // отключаем основную камеру после появления рига
        objectsInGame.Add(cameraMV.gameObject);
        
       

        //Создание синглтона Ввода данных от пользователя
        GameObject inputController = new GameObject();
        inputController.AddComponent<InputControl>();
        objectsInGame.Add(inputController);

        //Создание синглтона контроля за TimeScale
        GameObject timeFollowController = new GameObject();
        timeFollowController.AddComponent<TimeFollowController>();
        objectsInGame.Add(timeFollowController);

        // создаем окно оповещений
        alertsModelView = UIFactory.CreateAlertsModelView(canvas);
        objectsInGame.Add(alertsModelView.gameObject);


        //Создаём окно позиции в гонке:
        TrackPositionModelView trackposMenuModel = UIFactory.CreateTrackPositionModelView(canvas);
        trackposMenuModel.trackPath = checkpointsPath;
        objectsInGame.Add(trackposMenuModel.gameObject);

        // создаем миникарту
        GameObject minimapCamera = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/MinimapCamera"));
        objectsInGame.Add(minimapCamera);
        GameObject miniMap = UIFactory.CreateMinimapObj(canvas);
        objectsInGame.Add(miniMap);
        

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


    /// <summary>
    /// Меню финиша игрока в гонке
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HandleTrackFinish(object sender, EventArgs e) 
    {
        trackFinishMenuModel = UIFactory.CreateTrackFinishMenuModelView(canvas);
        trackFinishMenuModel.trackPath = checkpointsPath;
        objectsInGame.Add(trackFinishMenuModel.gameObject);
        CinemachineModelView.Instance.CountdownPause();
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

    /// <summary>
    /// Стартовый отсчет с паузой
    /// </summary>
    public void CountdownPause()
    {
        CinemachineModelView.Instance.CountdownPause();
        alertsModelView.ShowCountDown();
    }
}
