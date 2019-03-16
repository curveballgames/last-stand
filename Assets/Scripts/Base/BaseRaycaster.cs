using UnityEngine;
using Curveball;

namespace LastStand
{
    public class BaseRaycaster
    {
        private static int? roomLayermask;
        private static RaycastHit hitInfo;

        public static RoomModel CastForRooms()
        {
            if (!roomLayermask.HasValue)
            {
                roomLayermask = LayerMask.NameToLayer("Room");
            }

            if (Utilities.RaycastMousePosition(out hitInfo, roomLayermask.Value))
            {
                return hitInfo.collider.gameObject.GetComponent<RoomModel>();
            }

            return null;
        }
    }
}