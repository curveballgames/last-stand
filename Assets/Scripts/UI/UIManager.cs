using UnityEngine;
using Curveball;

namespace LastStand
{
    public class UIManager : CBGGameObject
    {
        public static UIManager Singleton;

        public GameObject MainMenuRoot;
        public GameObject CityHUD;
        public Transform SurvivorAssignParent;

        private void Awake()
        {
            Singleton = this;

            EventSystem.Subscribe<StartNewGameFadeEvent>(OnNewGameFadeStart, this);
            EventSystem.Subscribe<ShowDayOverviewEvent>(OnShowDayOverview, this);
            EventSystem.Subscribe<ViewInitialisedEvent>(OnViewInitialised, this);

            CityHUD.SetActive(false);
        }

        private void OnNewGameFadeStart(StartNewGameFadeEvent e)
        {
            MenuSystem.CloseActiveMenu(false);
        }

        private void OnViewInitialised(ViewInitialisedEvent e)
        {
            MainMenuRoot.SetActive(false);
            CityHUD.SetActive(true);
        }

        void OnShowDayOverview(ShowDayOverviewEvent e)
        {
            CityHUD.SetActive(true);
        }
    }
}