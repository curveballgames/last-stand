using Curveball;
using UnityEngine;

namespace LastStand
{
    public class CameraController : CBGGameObject
    {
        private static readonly Vector2 CITY_BOUNDS = new Vector2(30f, 20f);

        public TrackballCameraController BaseCameraController;
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
            BaseCameraController.Bounds = BaseModel.CurrentBase.Bounds;
            BaseCameraController.BoundsCenter = BaseModel.CurrentBase.Center;
            CityCameraController.Bounds = CITY_BOUNDS;
            CityCameraController.BoundsCenter = Vector3.zero;

            ControlMode = CameraControlMode.BaseView;
            SelectChildController();
        }

        void OnShowDayOverview(ShowDayOverviewEvent e)
        {
            ControlMode = e.Type == DayOverviewType.Base ? CameraControlMode.BaseView : CameraControlMode.CityView;
            SelectChildController();
        }

        void SelectChildController()
        {
            switch (ControlMode)
            {
                case CameraControlMode.BaseView:
                    BaseCameraController.enabled = true;
                    CityCameraController.enabled = false;
                    break;
                case CameraControlMode.CityView:
                    CityCameraController.enabled = true;
                    BaseCameraController.enabled = false;
                    break;
            }
        }
    }
}