﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// скрипт висит на пустом объекте Main
public class Main : MonoBehaviour
{
    public Canvas canvas;

    // точка входа в приложение
    void Start()
    {
        CreateMainMenu();

        // подписываемся на кнопки из меню
    }

    private void CreateMainMenu()
    {
       MainMenuModelView menuModelView = UIFactory.CreateMainMenuModelView(canvas);
       menuModelView.OnStart += HandleGameCreation;
       menuModelView.OnExit += HandleExitGame;
    }

    private void HandleGameCreation(object sender, EventArgs e)
    {
        Destroy(((MonoBehaviour)sender).gameObject);  // удаляем sender, т.е в нашем случае это объект со скриптом menuModelView ( но можно было его кэшировать и не приводить тип sender'а)
        StartGame();
    }

    private void HandleExitGame(object sender, EventArgs e)
    {
        Debug.Log("Exitting Game!");
    }

    private void StartGame()
    {
        GameController game = new GameController(canvas);
        game.OnDestroyGame += HandleOnGameDestroy;
    }

    private void HandleOnGameDestroy(object sender, EventArgs e)
    {
        CreateMainMenu();
    }
}