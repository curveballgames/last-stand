using UnityEngine;
using Curveball;

namespace LastStand
{
    public class BaseRaycaster : CBGGameObject
    {
        private static int? roomLayermask;
        private static RaycastHit hitInfo;

        private static RoomHoverEvent hoverEvent;

        private static bool castedThisFrame = false;

        private void Awake()
        {
            hoverEvent = new RoomHoverEvent();
        }

        private void Update()
        {
            CastForRooms();
        }

        private void LateUpdate()
        {
            castedThisFrame = false;
        }

        public static RoomModel GetHoveredRoom()
        {
            if (!castedThisFrame)
                CastForRooms();

            return hoverEvent.Room;
        }

        public static void CastForRooms()
        {
            if (castedThisFrame)
            {
                return;
            }

            hoverEvent.Room = null;

            if (!roomLayermask.HasValue)
            {
                roomLayermask = Utilities.GetLayerMaskForAllExcept("Room");
            }

            castedThisFrame = true;

            if (Utilities.RaycastMousePosition(out hitInfo, roomLayermask.Value))
            {
                hoverEvent.Room = hitInfo.collider.gameObject.GetComponent<RoomModel>();
            }

            EventSystem.Publish(hoverEvent);
        }
    }
}