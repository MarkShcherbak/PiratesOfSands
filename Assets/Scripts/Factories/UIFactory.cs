using UI;
using UnityEngine;

namespace Factories
{
    public class UIFactory
    {
        public static MainMenuModelView CreateMainMenuModelView(Canvas canvas)
        {
            GameObject mainMenuPrefab = Resources.Load<GameObject>("Prefabs/UI/MainMenuPanel");
            MainMenuModelView modelView = UnityEngine.Object.Instantiate(mainMenuPrefab, canvas.transform)
                .GetComponent<MainMenuModelView>();
            return modelView;
        }
    }
}