using UnityEngine;
using Curveball;

namespace LastStand
{
    public class CityRaycaster : CBGGameObject
    {
        private static int? roomLayermask;
        private static RaycastHit hitInfo;

        private static CityBuildingHoverEvent hoverEvent;

        private static bool castedThisFrame = false;

        private void Awake()
        {
            hoverEvent = new CityBuildingHoverEvent(null);
        }

        private void Update()
        {
            CastForBuildings();
        }

        private void LateUpdate()
        {
            castedThisFrame = false;
        }

        public static CityBuildingModel GetHoveredBuilding()
        {
            if (!castedThisFrame)
                CastForBuildings();

            return hoverEvent.Building;
        }

        public static void CastForBuildings()
        {
            if (castedThisFrame)
            {
                return;
            }

            hoverEvent.Building = null;

            if (!roomLayermask.HasValue)
            {
                roomLayermask = Utilities.GetLayerMaskForAllExcept("City Building");
            }

            castedThisFrame = true;

            if (Utilities.RaycastMousePosition(out hitInfo, roomLayermask.Value))
            {
                hoverEvent.Building = hitInfo.collider.gameObject.GetComponent<CityBuildingModel>();
            }

            EventSystem.Publish(hoverEvent);
        }
    }
}