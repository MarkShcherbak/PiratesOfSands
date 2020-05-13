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
        CreateMainMenu(canvas);

        // подписываемся на кнопки из меню
    }

    private void CreateMainMenu(Canvas canvas)
    {
        // создаем меню из фабрики UI
    }

    private void HandleGameStarted()
    {
        //создаем игру
    }
}
