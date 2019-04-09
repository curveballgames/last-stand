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
        public ScrollRect ScrollView;
        [Space]
        public SurvivorReportSection SurvivorSection;
        public ScavengerReportSection ScavengerSection;
        public ConstructionReportSection ConstructionSection;
        [Space]
        public Button ContinueButton;
        public TextMeshProUGUI ContinueButtonText;

        private void Awake()
        {
            EventSystem.Subscribe<ShowReportEvent>(OnShowReport, this);

            ContinueButton.onClick.AddListener(OnContinueClicked);
        }

        void OnShowReport(ShowReportEvent e)
        {
            Title.text = LocalisationManager.GetValue(TITLE_LOCALISATION_KEY + GameStateController.CurrentState.ToString().ToLower());
            ContinueButtonText.text = LocalisationManager.GetValue(ADVANCE_LOCALISATION_KEY + GameStateController.CurrentState.ToString().ToLower());

            SurvivorSection.UpdateView();
            ScavengerSection.UpdateView();
            ConstructionSection.UpdateView();

            ScrollView.verticalNormalizedPosition = 1f;

            CanvasFader.ForceShow();
        }

        void OnContinueClicked()
        {
            EventSystem.Publish(new AdvanceDayPeriodEvent());
            CanvasFader.FadeOut();
        }
    }
}