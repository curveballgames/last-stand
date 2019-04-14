using UnityEngine;
using UnityEngine.UI;
using Curveball;

namespace LastStand
{
    public class SurvivorAssignmentPointer : CBGUIComponent
    {
        public RawImage Headshots;
        public SurvivorModel Survivor { get; set; }
        public Button UnassignButton;

        private void Awake()
        {
            UnassignButton.onClick.AddListener(Unassign);
        }

        public void SetSurvivor(SurvivorModel model)
        {
            Survivor = model;

            if (model != null)
            {
                Headshots.texture = AvatarRenderCamera.RenderHeadshot(model);
            }
        }

        void Unassign()
        {
            if (Survivor == null)
                return;

            SurvivorModel replacedModel = Survivor;
            Survivor = null;
            replacedModel.AssignToBuilding(null);
        }
    }
}