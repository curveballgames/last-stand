using UnityEngine;
using Curveball;

namespace LastStand
{
    public class UIManager : CBGGameObject
    {
        public static UIManager Singleton;

        public GameObject MainMenuRoot;
        public GameObject BaseHUD;
        public GameObject CityHUD;

        public Transform BuildButtonParent;
        public Transform RoomAssignmentSlotsParent;
        public Transform RoomProgressBarParent;

        private void Awake()
        {
            Singleton = this;

            EventSystem.Subscribe<StartNewGameFadeEvent>(OnNewGameFadeStart, this);
            EventSystem.Subscribe<NewGameEvent>(OnNewGame, this);
            EventSystem.Subscribe<ShowDayOverviewEvent>(OnShowDayOverview, this);

            BaseHUD.SetActive(false);
            CityHUD.SetActive(false);
        }

        private void OnNewGameFadeStart(StartNewGameFadeEvent e)
        {
            MenuSystem.CloseActiveMenu(false);
        }

        private void OnNewGame(NewGameEvent e)
        {
            MainMenuRoot.SetActive(false);
            CityHUD.SetActive(false);
            BaseHUD.SetActive(true);
        }

        void OnShowDayOverview(ShowDayOverviewEvent e)
        {
            if (e.Type == DayOverviewType.Base)
            {

                BaseHUD.SetActive(true);
                CityHUD.SetActive(false);
            }
            else
            {
                BaseHUD.SetActive(false);
                CityHUD.SetActive(true);
            }
        }
    }
}