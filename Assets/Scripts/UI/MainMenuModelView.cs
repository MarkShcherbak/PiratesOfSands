using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuModelView : MonoBehaviour
    {
        public Button startButton;
        public Button exitButton;

        public event EventHandler OnStart = (sender, e) => { };
        public event EventHandler OnExit = (sender, e) => { };
        
        private void Start()
        {
            startButton.onClick.AddListener(delegate
            {
                OnStart(this, EventArgs.Empty);
            });
            exitButton.onClick.AddListener(delegate
            {
                OnExit(this, EventArgs.Empty);
            });
        }
    }
}