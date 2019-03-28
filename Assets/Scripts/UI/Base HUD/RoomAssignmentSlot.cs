using UnityEngine;
using UnityEngine.UI;
using Curveball;

namespace LastStand
{
    public class RoomAssignmentSlot : CBGUIComponent
    {
        public GameObject SurvivorIconRoot;
        public Image AssignmentColorImage;
        public Button UnassignButton;
        public RawImage Headshot;

        private SurvivorModel LinkedSurvivor;

        private void Awake()
        {
            UnassignButton.onClick.AddListener(OnUnassignClick);
        }

        public void ConfigureForSurvivor(SurvivorModel s, bool roomIsBuilt)
        {
            LinkedSurvivor = s;
            SurvivorIconRoot.SetActive(s != null);

            if (s != null)
            {
                AssignmentColorImage.color = SurvivorAvatarGenerator.GetColorForRoom(s.AssignedRoom);
                Headshot.texture = AvatarRenderCamera.RenderHeadshot(LinkedSurvivor);
            }
            else
            {
                AssignmentColorImage.color = SurvivorAvatarGenerator.GetColorForRoom(null);
                Headshot.texture = null;
            }

            UnassignButton.gameObject.SetActive(s != null);
        }

        void OnUnassignClick()
        {
            if (LinkedSurvivor != null)
            {
                LinkedSurvivor.AssignRoom(null);
            }
        }
    }
}