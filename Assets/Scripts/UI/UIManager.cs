using UnityEngine;
using Curveball;

namespace LastStand
{
    public class UIManager : CBGGameObject
    {
        public static UIManager Singleton;

        public GameObject MainMenuRoot;
        public GameObject BaseHUD;

        public Transform BuildButtonParent;
        public Transform RoomAssignmentSlotsParent;
        public Transform RoomProgressBarParent;

        private void Awake()
        {
            Singleton = this;

            EventSystem.Subscribe<StartNewGameFadeEvent>(OnNewGameFadeStart, this);
            EventSystem.Subscribe<NewGameEvent>(OnNewGame, this);

            BaseHUD.SetActive(false);
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