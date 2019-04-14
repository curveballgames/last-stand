using UnityEngine;
using Curveball;

namespace LastStand
{
    public class DayCameraController : TrackballCameraController
    {
        protected bool isAnimationInterruptable = false;

        protected virtual void Awake()
        {
            EventSystem.Subscribe<ZoomToTargetEvent>(OnZoomToTarget, this);
            EventSystem.Subscribe<ConfirmAssignmentEvent>(OnConfirmAssignment, this);
            EventSystem.Subscribe<DayPeriodUpdatedEvent>(OnDayPeriodUpdated, this);
        }

        protected override void UpdateCameraPosition()
        {
            if (isAnimationInterruptable && !Mathf.Approximately(0f, HorizontalMovementAxis) && !Mathf.Approximately(0f, VerticalMovementAxis))
            {
                animationTarget = null;
            }

            base.UpdateCameraPosition();
        }

        void OnZoomToTarget(ZoomToTargetEvent e)
        {
            if (e.Survivor != null)
            {
                CityBuildingModel target = e.Survivor.AssignedBuilding != null ? e.Survivor.AssignedBuilding : CityBuildingModel.CurrentBase;
                animationTarget = target.transform;
            }
        }

        void OnConfirmAssignment(ConfirmAssignmentEvent e)
        {
            isAnimationInterruptable = false;
        }

        void OnDayPeriodUpdated(DayPeriodUpdatedEvent e)
        {
            isAnimationInterruptable = GameStateController.CurrentState == GameState.Morning || GameStateController.CurrentState == GameState.Afternoon;
        }
    }
}