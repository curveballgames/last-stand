using Curveball;
using TMPro;
using UnityEngine;

namespace LastStand
{
    public class CityBuildingInfoPanel : CBGUIComponent
    {
        private static readonly AmountLimit[] DANGER_LIMITS =
        {
            new AmountLimit(0, "city-ui:danger-none"),
            new AmountLimit(3, "city-ui:danger-low"),
            new AmountLimit(6, "city-ui:danger-medium"),
            new AmountLimit(10, "city-ui:danger-high")
        };

        private static readonly AmountLimit[] RESOURCE_LIMITS =
        {
            new AmountLimit(0, "city-ui:resources-none"),
            new AmountLimit(5, "city-ui:resources-low"),
            new AmountLimit(10, "city-ui:resources-medium"),
            new AmountLimit(15, "city-ui:resources-high")
        };

        public TextMeshProUGUI Title;
        public TextMeshProUGUI DangerLabel;
        public TextMeshProUGUI FoodLabel;
        public TextMeshProUGUI BuildingResourcesLabel;
        public TextMeshProUGUI SecuredText;
        public ChunkedStatBar ExploredBar;
        public CanvasGroupFader Fader;

        private CityBuildingModel currentModel;

        private void Awake()
        {
            EventSystem.Subscribe((NewGameEvent e) => { currentModel = null; }, this);
            EventSystem.Subscribe<CityBuildingHoverEvent>(OnCityBuildingHovered, this);
            EventSystem.Subscribe<SurvivorAssignmentUpdatedEvent>(OnSurvivorAssignmentUpdated, this);
        }

        void OnCityBuildingHovered(CityBuildingHoverEvent e)
        {
            ConfigureForBuilding(e.Building);
        }

        void OnSurvivorAssignmentUpdated(SurvivorAssignmentUpdatedEvent e)
        {
            if (e.CityBuilding != null && e.CityBuilding == currentModel)
            {
                ConfigureForBuilding(currentModel);
            }
        }

        void ConfigureForBuilding(CityBuildingModel model)
        {
            currentModel = model;

            if (model == null)
            {
                Fader.FadeOut();
                return;
            }

            Title.text = model.Name;

            ConfigureDangerText(model);
            ConfigureFoodText(model);
            ConfigureMaterialsText(model);
            ConfigureExplorationBar(model);

            Fader.FadeIn();
        }

        void ConfigureDangerText(CityBuildingModel model)
        {
            if (model.IsExplored)
            {
                DangerLabel.text = "-";
                return;
            }

            DangerLabel.text = GetLimitString(model.GetDangerLevel(), DANGER_LIMITS);
        }

        void ConfigureFoodText(CityBuildingModel model)
        {
            FoodLabel.text = GetLimitString(model.GetFoodRemaining(), RESOURCE_LIMITS);
        }

        void ConfigureMaterialsText(CityBuildingModel model)
        {
            BuildingResourcesLabel.text = GetLimitString(model.GetBuildingMaterialsRemaining(), RESOURCE_LIMITS);
        }

        void ConfigureExplorationBar(CityBuildingModel model)
        {
            if (model.IsExplored)
            {
                ExploredBar.MaxValue = Mathf.Max(model.RequiredExploreStages, 1);
                ExploredBar.Value = Mathf.Max(model.StagesExplored, 1);
            }
            else
            {
                ExploredBar.MaxValue = model.RequiredExploreStages;
                ExploredBar.Value = model.StagesExplored;

                if (model.AssignedSurvivors.Count > 0)
                {
                    ExploredBar.PreviewValue = model.AssignedSurvivors.Count;
                }
                else
                {
                    ExploredBar.PreviewValue = 0;
                }
            }


            ExploredBar.ForceUpdate();

            SecuredText.gameObject.SetActive(model.IsExplored);
        }

        private string GetLimitString(int limitAmount, AmountLimit[] lookupArray)
        {
            for (int i = 0; i < lookupArray.Length; i++)
            {
                if (limitAmount <= lookupArray[i].Limit)
                {
                    return LocalisationManager.GetValue(lookupArray[i].LocalisationKey);
                }
            }

            return LocalisationManager.GetValue(lookupArray[lookupArray.Length - 1].LocalisationKey);
        }

        private struct AmountLimit
        {
            public int Limit;
            public string LocalisationKey;

            public AmountLimit(int limit, string localisationKey)
            {
                Limit = limit;
                LocalisationKey = localisationKey;
            }
        }
    }
}