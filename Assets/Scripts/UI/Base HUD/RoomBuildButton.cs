using UnityEngine;
using Curveball;
using UnityEngine.UI;

namespace LastStand
{
    public class RoomBuildButton : CBGUIComponent
    {
        private static readonly Vector3 WORLD_OFFSET = Vector3.up * 2f;

        public Button BuildButton;
        public RoomView LinkedRoom { get; set; }

        private void Awake()
        {
            BuildButton.onClick.AddListener(OnClick);
        }

        private void LateUpdate()
        {
            RectTransform.position = Camera.main.WorldToScreenPoint(LinkedRoom.transform.position + WORLD_OFFSET);
        }

        void OnClick()
        {
            EventSystem.Publish(new BuildButtonPressedEvent(LinkedRoom.LinkedModel));
        }
    }
}