using System;
using System.Collections;
using System.Collections.Generic;
using Factories;
using UI;
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
       menuModelView.OnStart += HandleGameStarted;
       menuModelView.OnExit += HandleExitGame;
    }

    private void HandleGameStarted(object sender, EventArgs e)
    {
        Destroy(((MonoBehaviour)sender).gameObject);
        StartGame();
    }

    private void HandleExitGame(object sender, EventArgs e)
    {
        Debug.Log("Exitting Game!");
    }

    private void StartGame()
    {
        Debug.Log("Game Started!");
    }
}
