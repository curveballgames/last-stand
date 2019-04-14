using UnityEngine;
using UnityEngine.UI;
using Curveball;

namespace LastStand
{
    public class SurvivorAssignmentPointer : CBGUIComponent
    {
        public RawImage Headshots;
        public SurvivorModel Survivor { get; set; }
        public Button UnassignButton;

        private void Awake()
        {
            UnassignButton.onClick.AddListener(Unassign);

            EventSystem.Subscribe<ConfirmAssignmentEvent>(OnConfirmAssignment, this);
            EventSystem.Subscribe<DayPeriodUpdatedEvent>(OnDayPeriodAdvanced, this);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<ConfirmAssignmentEvent>(OnConfirmAssignment, this);
            EventSystem.Unsubscribe<DayPeriodUpdatedEvent>(OnDayPeriodAdvanced, this);
        }

        public void SetSurvivor(SurvivorModel model)
        {
            Survivor = model;

            if (model != null)
            {
                Headshots.texture = AvatarRenderCamera.RenderHeadshot(model);
            }
        }

        void Unassign()
        {
            if (!GameStateController.IsInDayManagementState())
                return;

            if (Survivor == null)
                return;

            SurvivorModel replacedModel = Survivor;
            Survivor = null;
            replacedModel.AssignToBuilding(null);
        }

        void OnConfirmAssignment(ConfirmAssignmentEvent e)
        {
            UnassignButton.gameObject.SetActive(false);
        }

        void OnDayPeriodAdvanced(DayPeriodUpdatedEvent e)
        {
            UnassignButton.gameObject.SetActive(GameStateController.IsInDayManagementState());
        }
    }
}