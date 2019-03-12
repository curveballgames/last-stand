using Curveball;

namespace LastStand
{
    public class CameraController : CBGGameObject
    {
        public BaseCameraController BaseCameraController;

        private CameraControlMode ControlMode;

        private void Awake()
        {
            EventSystem.Subscribe<ViewInitialisedEvent>(OnViewInitialised, this);
            ControlMode = CameraControlMode.Static;
        }

        void OnViewInitialised(ViewInitialisedEvent e)
        {
            ControlMode = CameraControlMode.BaseView;
            SelectChildController();
        }

        void SelectChildController()
        {
            switch (ControlMode)
            {
                case CameraControlMode.BaseView:
                    BaseCameraController.enabled = true;
                    break;
            }
        }
    }
}