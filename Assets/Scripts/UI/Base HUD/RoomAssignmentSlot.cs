using UnityEngine;
using UnityEngine.UI;
using Curveball;

namespace LastStand
{
    public class RoomAssignmentSlot : CBGUIComponent
    {
        public GameObject SurvivorIconRoot;
        public Image AssignmentColorImage;

        public void ConfigureForSurvivor(SurvivorModel s, bool roomIsBuilt)
        {
            SurvivorIconRoot.SetActive(s != null);

            if (s != null)
            {
                AssignmentColorImage.color = SurvivorAvatarGenerator.GetColorForRoom(s.AssignedRoom);
            }
            else
            {
                AssignmentColorImage.color = SurvivorAvatarGenerator.GetColorForRoom(null);
            }
        }
    }
}