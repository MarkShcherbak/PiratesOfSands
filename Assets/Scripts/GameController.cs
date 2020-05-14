using System;
using System.Collections.Generic;
using UnityEngine;

// класс игры, создает игру
public class GameController
{
    public event EventHandler OnDestroyGame = (sender, e) => { };
    
    private TrackModelView trackMV;  // ссылка на трек trackModelView;
    private Canvas canvas;
    private PauseMenuModelView pauseMV = null; // ссылка на UI окошко паузы
    private List<GameObject> objectsInGame; // лист объектов (нужен для удаления при выходе в меню)
    
    
        public GameController(Canvas mainCanvas) // конструктор игры, можно сделать несколько конструкторов(например сколько противников, какая сложность, какая трасса)
        {
            objectsInGame = new List<GameObject>();
            canvas = mainCanvas;
            trackMV =  TrackFactory.CreateTestTrackModelView(); // создаем трассу и добавляем в лист объектов в игре
            trackMV.OnPause += HandleGamePause; // подписываем обработчик паузы на событие паузы
            objectsInGame.Add(trackMV.gameObject);
            // TODO создаем чекпоинты

            // TODO создаем игрока

            // TODO создаем AI и корабли AI
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
