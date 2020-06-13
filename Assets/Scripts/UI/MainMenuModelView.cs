using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

    
    // панель главного меню - скрипт висит на префабре в папке Resources
    public class MainMenuModelView : MonoBehaviour
    {
        public Button startButton;
        public Button exitButton;

        public Text lapsCountText;
        public Text lapsCountOutlineText;

        public Text shipsCountText;
        public Text shipsCountOutlineText;

        public event EventHandler OnStart = (sender, e) => { };
        public event EventHandler OnExit = (sender, e) => { };

        private int lapsCount = 1;
        private int shipsCount = 5;
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

        public void IncreaseLapCount()
        {
            if(lapsCount < 50)
            lapsCount++;

            lapsCountText.text = lapsCount.ToString();
            lapsCountOutlineText.text = lapsCount.ToString();
        }
        
        public void DecreaseLapCount()
        {
            if(lapsCount > 1)
                lapsCount--;

            lapsCountText.text = lapsCount.ToString();
            lapsCountOutlineText.text = lapsCount.ToString();
        }
        
        public void IncreaseShipCount()
        {
            if(shipsCount < 20)
                shipsCount++;

            shipsCountText.text = shipsCount.ToString();
            shipsCountOutlineText.text = shipsCount.ToString();
        }
        
        public void DecreaseShipCount()
        {
            if(shipsCount > 1)
                shipsCount--;

            shipsCountText.text = shipsCount.ToString();
            shipsCountOutlineText.text = shipsCount.ToString();
        }
    }
