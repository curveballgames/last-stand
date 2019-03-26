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

        public TextMeshProUGUI Title;
        public TextMeshProUGUI DangerLabel;
        public TextMeshProUGUI AmountExploredLabel;
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
            DangerLabel.text = GetDangerText(model);
            AmountExploredLabel.text = string.Format("{0}/{1}", model.StagesExplored, model.RequiredExploreStages);

            Fader.FadeIn();
        }

        string GetDangerText(CityBuildingModel model)
        {
            return GetLimitString(model.DangerLevel, DANGER_LIMITS);
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