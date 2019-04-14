using Curveball;
using UnityEngine;

namespace LastStand
{
    public class CameraController : CBGGameObject
    {
        private static readonly Vector2 CITY_BOUNDS = new Vector2(30f, 20f);
        
        public TrackballCameraController CityCameraController;

        private CameraControlMode ControlMode;

        private void Awake()
        {
            EventSystem.Subscribe<ViewInitialisedEvent>(OnViewInitialised, this);
            EventSystem.Subscribe<ShowDayOverviewEvent>(OnShowDayOverview, this);
            ControlMode = CameraControlMode.Static;
        }

        void OnViewInitialised(ViewInitialisedEvent e)
        {
            CityCameraController.Bounds = CITY_BOUNDS;
            CityCameraController.BoundsCenter = Vector3.zero;

            ControlMode = CameraControlMode.CityView;
            SelectChildController();
        }

        void OnShowDayOverview(ShowDayOverviewEvent e)
        {
            ControlMode = CameraControlMode.CityView;
            SelectChildController();
        }

        void SelectChildController()
        {
            switch (ControlMode)
            {
                case CameraControlMode.CityView:
                    CityCameraController.enabled = true;
                    break;
            }
        }
    }
}