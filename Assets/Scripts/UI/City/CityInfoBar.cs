﻿using TMPro;
using Curveball;
using UnityEngine.UI;

namespace LastStand
{
    public class CityInfoBar : CBGUIComponent
    {
        private static readonly string DAY_STRING_FORMAT = "{0} {1}";
        private static readonly string DAY_LOCALISATION_KEY = "general-ui:day";

        private static readonly string POPULATION_STRING_FORMAT = "{0}/{1}";

        public TextMeshProUGUI DayText;
        public TextMeshProUGUI PopulationText;
        public TextMeshProUGUI FoodText;
        public TextMeshProUGUI BuildingMaterialsText;
        public Button ConfirmAssignmentButton;

        private void Awake()
        {
            EventSystem.Subscribe<PlayerResourcesUpdatedEvent>(OnResourcesUpdated, this);
            EventSystem.Subscribe<DayPeriodUpdatedEvent>(OnDayPeriodAdvanced, this);

            ConfirmAssignmentButton.onClick.AddListener(OnConfirmClick);
        }

        private void OnEnable()
        {
            if (PlayerResources.Singleton != null)
                UpdateDisplay();
        }

        void OnResourcesUpdated(PlayerResourcesUpdatedEvent e)
        {
            UpdateDisplay();
        }

        void UpdateDisplay()
        {
            DayText.text = string.Format(DAY_STRING_FORMAT, LocalisationManager.GetValue(DAY_LOCALISATION_KEY), 1);
            PopulationText.text = string.Format(POPULATION_STRING_FORMAT, SurvivorModel.AllModels.Count, "TODO");
            FoodText.text = PlayerResources.Singleton.Food.ToString();
            BuildingMaterialsText.text = PlayerResources.Singleton.BuildingMaterials.ToString();
        }

        void OnConfirmClick()
        {
            ConfirmAssignmentButton.interactable = false;
            EventSystem.Publish(new ConfirmAssignmentEvent());
        }

        void OnDayPeriodAdvanced(DayPeriodUpdatedEvent e)
        {
            if (GameStateController.CurrentState == GameState.Morning || GameStateController.CurrentState == GameState.Afternoon)
            {
                ConfirmAssignmentButton.interactable = true;
            }
        }
    }
}