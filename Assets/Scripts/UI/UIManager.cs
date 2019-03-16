using UnityEngine;
using Curveball;

namespace LastStand
{
    public class UIManager : CBGGameObject
    {
        public GameObject MainMenuRoot;
        public GameObject BaseHUD;

        private void Awake()
        {
            EventSystem.Subscribe<StartNewGameFadeEvent>(OnNewGameFadeStart, this);
            EventSystem.Subscribe<NewGameEvent>(OnNewGame, this);
        }

        private void OnNewGameFadeStart(StartNewGameFadeEvent e)
        {
            MenuSystem.CloseActiveMenu(false);
        }

        private void OnNewGame(NewGameEvent e)
        {
            MainMenuRoot.SetActive(false);
            BaseHUD.SetActive(true);
        }
    }
}