using Curveball;
using TMPro;

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
        public ChunkedStatBar ExploredBar;
        public CanvasGroupFader Fader;

        private void Awake()
        {
            EventSystem.Subscribe<CityBuildingHoverEvent>(OnCityBuildingHovered, this);
        }

        void OnCityBuildingHovered(CityBuildingHoverEvent e)
        {
            ConfigureForBuilding(e.Building);
        }

        void ConfigureForBuilding(CityBuildingModel model)
        {
            if (model == null)
            {
                Fader.FadeOut();
                return;
            }

            Title.text = model.Name;

            ConfigureDangerText(model);
            ConfigureFoodText(model);
            ConfigureMaterialsText(model);

            ExploredBar.MaxValue = model.RequiredExploreStages;
            ExploredBar.Value = model.StagesExplored;
            ExploredBar.ForceUpdate();

            Fader.FadeIn();
        }

        void ConfigureDangerText(CityBuildingModel model)
        {
            DangerLabel.text = GetLimitString(model.DangerLevel, DANGER_LIMITS);
        }

        void ConfigureFoodText(CityBuildingModel model)
        {
            FoodLabel.text = GetLimitString(model.GetFoodRemaining(), RESOURCE_LIMITS);
        }

        void ConfigureMaterialsText(CityBuildingModel model)
        {
            BuildingResourcesLabel.text = GetLimitString(model.GetBuildingMaterialsRemaining(), RESOURCE_LIMITS);
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