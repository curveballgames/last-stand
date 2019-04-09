using UnityEngine;
using UnityEngine.UI;
using Curveball;
using TMPro;

namespace LastStand
{
    public class RoomInfoPanel : CBGUIComponent
    {
        private static readonly string RECLAIM_UNFINISHED_LOCALISATION_KEY = "build-ui:cancel-construction-content";
        private static readonly string RECLAIM_BUILT_LOCALISATION_KEY = "base-ui:reclaim-built-content";
        private static readonly string RECLAIM_CONFIRM_LOCALISATION_KEY = "base-ui:reclaim-button";
        private static readonly string RECLAIM_CANCEL_LOCALISATION_KEY = "general-ui:cancel";

        public TextMeshProUGUI Title;
        public TextMeshProUGUI Description;
        public CanvasGroupFader Fader;

        public Button ReclaimButton;
        public RoomAssignmentSlot[] AssignmentSlots;

        private RoomModel linkedModel;

        private void Awake()
        {
            ReclaimButton.onClick.AddListener(OnReclaimClick);
            EventSystem.Subscribe<RoomHoverEvent>(OnRoomHovered, this);
            EventSystem.Subscribe<RoomSelectEvent>(OnRoomSelectionChanged, this);
            EventSystem.Subscribe<SurvivorAssignmentUpdatedEvent>(OnSurvivorAssignmentUpdated, this);
        }

        void OnRoomHovered(RoomHoverEvent e)
        {
            if (RoomSelectionManager.SelectedRoom != null)
                return;

            ConfigureForModel(e.Room);

            if (linkedModel == null)
            {
                ConfigureForModel(RoomSelectionManager.SelectedRoom);
            }

            UpdateVisibility();
        }

        void OnRoomSelectionChanged(RoomSelectEvent e)
        {
            ConfigureForModel(e.Model);

            if (linkedModel == null)
            {
                ConfigureForModel(BaseRaycaster.GetHoveredRoom());
            }

            UpdateVisibility();
        }

        void OnSurvivorAssignmentUpdated(SurvivorAssignmentUpdatedEvent e)
        {
            if (linkedModel == null)
                return;

            ConfigureForModel(linkedModel);
        }

        void ConfigureForModel(RoomModel model)
        {
            linkedModel = model;

            if (model == null)
                return;

            ReclaimButton.gameObject.SetActive(model.RoomType != RoomType.Empty && model.Reclaimable);
            Title.text = LocalisationManager.GetValue(RoomTypeDictionary.RoomNameLocalisationKeys[model.RoomType]);
            Description.text = LocalisationManager.GetValue(RoomTypeDictionary.RoomDescriptionLocalisationKeys[model.RoomType]);

            int totalAssignable = model.AssignmentSlots;
            int counter = 0;

            foreach (SurvivorModel survivor in model.AssignedSurvivors)
            {
                AssignmentSlots[counter].ConfigureForSurvivor(survivor, model.IsBuilt);
                AssignmentSlots[counter].SetActive(true);
                counter++;
            }

            for (; counter < AssignmentSlots.Length; counter++)
            {
                AssignmentSlots[counter].ConfigureForSurvivor(null, model.IsBuilt);
                AssignmentSlots[counter].SetActive(counter < totalAssignable);
            }
        }

        void OnReclaimClick()
        {
            if (linkedModel == null || linkedModel.RoomType == RoomType.Empty || !linkedModel.Reclaimable)
                return;

            string content = LocalisationManager.GetValue(RECLAIM_BUILT_LOCALISATION_KEY);

            if (!linkedModel.IsBuilt)
                content = LocalisationManager.GetValue(RECLAIM_UNFINISHED_LOCALISATION_KEY);

            string okButton = LocalisationManager.GetValue(RECLAIM_CONFIRM_LOCALISATION_KEY);
            string cancelButton = LocalisationManager.GetValue(RECLAIM_CANCEL_LOCALISATION_KEY);

            EventSystem.Publish(new ShowModalEvent(content, okButton, OnConfimReclaimModal, cancelButton, null));
        }

        void OnConfimReclaimModal()
        {
            if (linkedModel == null)
                return;

            linkedModel.CancelConstruction();
            ConfigureForModel(linkedModel);
        }

        void UpdateVisibility()
        {
            if (linkedModel == null)
            {
                Fader.FadeOut();
            }
            else
            {
                Fader.FadeIn();
            }
        }
    }
}