using UnityEngine;
using Curveball;

namespace LastStand
{
    public class RoomSelectionManager : CBGGameObject
    {
        public static RoomModel SelectedRoom { get => roomSelectEvent.Model; }
        private static RoomSelectEvent roomSelectEvent = new RoomSelectEvent();

        private void Update()
        {
            if (Input.GetButtonDown("Cancel") && roomSelectEvent.Model != null)
            {
                roomSelectEvent.Model = null;
                EventSystem.Publish(roomSelectEvent);
            }

            if (Input.GetButtonDown("Select") && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if (BaseRaycaster.GetHoveredRoom() != null)
                {
                    roomSelectEvent.Model = BaseRaycaster.GetHoveredRoom();
                }
                else
                {
                    roomSelectEvent.Model = null;
                }

                EventSystem.Publish(roomSelectEvent);
            }
        }
    }
}