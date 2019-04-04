using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curveball;
using UnityEngine.UI;
using TMPro;

namespace LastStand
{
    public class DayReportUIController : CBGUIComponent
    {
        private static readonly string TITLE_LOCALISATION_KEY = "report-ui:title-";
        private static readonly string ADVANCE_LOCALISATION_KEY = "report-ui:advance-";

        public CanvasGroupFader CanvasFader;
        public TextMeshProUGUI Title;
        public Transform SurvivorPanelParent;
        public Button ContinueButton;
        public TextMeshProUGUI ContinueButtonText;

        private static List<SurvivorReportPanel> SurvivorPanels;

        private void Awake()
        {
            SurvivorPanels = new List<SurvivorReportPanel>();

            EventSystem.Subscribe<ShowReportEvent>(OnShowReport, this);

            ContinueButton.onClick.AddListener(OnContinueClicked);
        }

        void OnShowReport(ShowReportEvent e)
        {
            int counter = 0;

            foreach (SurvivorModel model in SurvivorModel.AllModels)
            {
                if (SurvivorPanels.Count <= counter)
                {
                    CreateNewSurvivorPanel();
                }

                SurvivorPanels[counter].ConfigureForSurvivor(model);
                SurvivorPanels[counter].SetActive(true);
                counter++;
            }

            for (; counter < SurvivorPanels.Count; counter++)
            {
                SurvivorPanels[counter].SetActive(false);
            }

            Title.text = LocalisationManager.GetValue(TITLE_LOCALISATION_KEY + GameStateController.CurrentState.ToString().ToLower());
            ContinueButtonText.text = LocalisationManager.GetValue(ADVANCE_LOCALISATION_KEY + GameStateController.CurrentState.ToString().ToLower());

            CanvasFader.ForceShow();
        }

        void CreateNewSurvivorPanel()
        {
            GameObject newSurvivorPanel = Instantiate(PrefabDictionary.Singleton.SurvivorReportPanelPrefab.gameObject, SurvivorPanelParent);
            SurvivorPanels.Add(newSurvivorPanel.GetComponent<SurvivorReportPanel>());
        }

        void OnContinueClicked()
        {
            EventSystem.Publish(new AdvanceDayEvent());
            CanvasFader.FadeOut();
        }
    }
}