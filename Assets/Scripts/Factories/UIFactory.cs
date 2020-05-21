using UnityEngine;

// фабрика UI, создает заранее подготовленные UI элементы из папки Recources
    public class UIFactory
    {
        public static MainMenuModelView CreateMainMenuModelView(Canvas canvas)
        {
            GameObject mainMenuPrefab = Resources.Load<GameObject>("Prefabs/UI/MainMenuPanel");
            MainMenuModelView modelView = UnityEngine.Object.Instantiate(mainMenuPrefab, canvas.transform)
                .GetComponent<MainMenuModelView>();
            return modelView;
        }
        
        public static PauseMenuModelView CreatePauseMenuModelView(Canvas canvas)
        {
            GameObject pauseMenuPrefab = Resources.Load<GameObject>("Prefabs/UI/PauseMenuPanel");
            PauseMenuModelView modelView = UnityEngine.Object.Instantiate(pauseMenuPrefab, canvas.transform)
                .GetComponent<PauseMenuModelView>();
            return modelView;
        }

        public static AbilityHUDModelView CreatePlayerAbilityUI(Canvas canvas)
        {
            GameObject abilityHUDPrefab = Resources.Load<GameObject>("Prefabs/UI/PlayerAbilityUI");
            AbilityHUDModelView modelView = UnityEngine.Object.Instantiate(abilityHUDPrefab, canvas.transform)
                .GetComponent<AbilityHUDModelView>();
            return modelView;
        }
    }
