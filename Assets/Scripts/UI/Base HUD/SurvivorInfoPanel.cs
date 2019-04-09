using Curveball;
using TMPro;
using UnityEngine;

namespace LastStand
{
    public class SurvivorInfoPanel : CBGGameObject
    {
        public CanvasGroupFader Fader;
        public TextMeshProUGUI SurvivorName;
        public ChunkedStatBar TirednessBar;
        public ChunkedStatBar HungerBar;
        public SurvivorStatBar ShootingBar;
        public SurvivorStatBar FitnessBar;
        public SurvivorStatBar StrengthBar;

        private SurvivorModel currentModel;

        private void Awake()
        {
            EventSystem.Subscribe<SurvivorIconHoveredEvent>(OnSurvivorIconHovered, this);
            EventSystem.Subscribe<SurvivorIconUnhoveredEvent>(OnSurvivorIconUnhovered, this);
            EventSystem.Subscribe<RoomHoverEvent>(OnRoomHovered, this);
        }

        void OnSurvivorIconHovered(SurvivorIconHoveredEvent e)
        {
            if (e.Icon.Model != currentModel)
            {
                ConfigureAndShowForModel(e.Icon.Model, e.Icon.Model.AssignedRoom);
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
        
        void OnRoomHovered(RoomHoverEvent e)
        {
            if (currentModel == null)
                return;

            RoomModel roomToUse = e.Room;

            if (roomToUse == null)
            {
                roomToUse = currentModel.AssignedRoom;
            }

            ConfigureAndShowForModel(currentModel, roomToUse);
        }

        void ConfigureAndShowForModel(SurvivorModel model, RoomModel room)
        {
            currentModel = model;

            RoomStatModifiers statModifiers = room == null ? RoomTypeDictionary.StatModifiers[RoomType.Empty] : RoomTypeDictionary.StatModifiers[room.RoomType];
            statModifiers.TirednessChange = room == null ? 0 : statModifiers.TirednessChange;

            SurvivorName.text = model.Name;
            ShootingBar.ConfigureForModel(model, model.ShootingSkill, statModifiers.ShootingChange);
            FitnessBar.ConfigureForModel(model, model.FitnessSkill, statModifiers.FitnessChange);
            StrengthBar.ConfigureForModel(model, model.StrengthSkill, statModifiers.StrengthChange);

            HungerBar.MaxValue = SurvivorModel.MAX_HUNGER;
            HungerBar.Value = model.Hunger;
            HungerBar.ForceUpdate();

            TirednessBar.MaxValue = SurvivorModel.MAX_TIREDNESS;
            TirednessBar.Value = Mathf.Min(model.Tiredness, model.Tiredness + statModifiers.TirednessChange);
            TirednessBar.PreviewValue = Mathf.Abs(statModifiers.TirednessChange);
            TirednessBar.ForceUpdate();

            Fader.FadeIn();
        }

        void Hide(bool immediate)
        {
            Fader.FadeOut();
        }
    }
}