using Curveball;
using TMPro;
using UnityEngine;

namespace LastStand
{
    public class SurvivorInfoPanel : CBGGameObject
    {
        public const float HIDE_TIMEOUT = 0.25f;

        public CanvasGroupFader Fader;
        public TextMeshProUGUI SurvivorName;
        public ChunkedStatBar TirednessBar;
        public ChunkedStatBar HungerBar;
        public SurvivorStatBar ShootingBar;
        public SurvivorStatBar FitnessBar;
        public SurvivorStatBar StrengthBar;

        private SurvivorModel currentModel;
        private Timer hideTimer;

        private void Awake()
        {
            EventSystem.Subscribe<SurvivorIconHoveredEvent>(OnSurvivorIconHovered, this);
            EventSystem.Subscribe<SurvivorIconUnhoveredEvent>(OnSurvivorIconUnhovered, this);
            EventSystem.Subscribe<CityBuildingHoverEvent>(OnCityBuildingHovered, this);

            hideTimer = Timer.CreateTimer(gameObject, HIDE_TIMEOUT, () =>
            {
                Fader.FadeOut();
            }, null, false, false, false, false, true);
        }

        void OnSurvivorIconHovered(SurvivorIconHoveredEvent e)
        {
            ClearHideTimer();

            if (e.Icon.Model != currentModel)
            {
                ConfigureAndShowForModel(e.Icon.Model, e.Icon.Model.AssignedBuilding);
            }
        }

        void OnSurvivorIconUnhovered(SurvivorIconUnhoveredEvent e)
        {
            if (e.Icon.Model == currentModel)
            {
                currentModel = null;
                Hide(false);
            }
        }
        
        void OnCityBuildingHovered(CityBuildingHoverEvent e)
        {
            if (currentModel == null)
                return;

            CityBuildingModel cityBuilding = e.Building;

            if (cityBuilding == null)
            {
                cityBuilding = currentModel.AssignedBuilding;
            }

            ConfigureAndShowForModel(currentModel, cityBuilding);
        }

        void ConfigureAndShowForModel(SurvivorModel model, CityBuildingModel building)
        {
            currentModel = model;

            CityBuildingStatModifiers statModifiers = new CityBuildingStatModifiers();

            if (building == null)
            {
                statModifiers = CityBuildingModel.CurrentBase.StatModifiers;
            }
            else if (!building.IsExplored)
            {
                statModifiers = CityBuildingModel.ScavengingModifiers;
            }
            else if (building.IsExplored)
            {
                statModifiers = building.StatModifiers;
            }

            SurvivorName.text = model.Name;
            ShootingBar.ConfigureForModel(model, model.ShootingSkill, statModifiers.ShootingChange);
            FitnessBar.ConfigureForModel(model, model.FitnessSkill, statModifiers.FitnessChange);
            StrengthBar.ConfigureForModel(model, model.StrengthSkill, statModifiers.StrengthChange);

            HungerBar.MaxValue = SurvivorModel.MAX_HUNGER;
            HungerBar.Value = model.Hunger;
            HungerBar.PreviewValue = 1;
            HungerBar.ForceUpdate();

            TirednessBar.MaxValue = SurvivorModel.MAX_TIREDNESS;
            TirednessBar.Value = Mathf.Min(model.Tiredness, model.Tiredness + statModifiers.TirednessChange);
            TirednessBar.PreviewValue = Mathf.Abs(statModifiers.TirednessChange);
            TirednessBar.ForceUpdate();

            Fader.FadeIn();
        }

        void Hide(bool immediate)
        {
            hideTimer.StartTimer(true);
        }

        void ClearHideTimer()
        {
            hideTimer.StopTimer();
        }
    }
}