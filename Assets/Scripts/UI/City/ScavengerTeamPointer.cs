using UnityEngine;
using UnityEngine.UI;
using Curveball;

namespace LastStand
{
    public class ScavengerTeamPointer : CBGUIComponent
    {
        private static readonly Vector3 BUILDING_OFFSET = new Vector3(0f, 4f, 0f);

        public RawImage Headshots;
        public CityBuildingModel BuildingToTrack;
        public Button UnassignButton;

        private void Awake()
        {
            UnassignButton.onClick.AddListener(Unassign);
        }

        private void LateUpdate()
        {
            if (BuildingToTrack == null)
                return;

            transform.position = Camera.main.WorldToScreenPoint(BuildingToTrack.transform.position + BUILDING_OFFSET);
        }

        public void SetHeadshotTexture(RenderTexture texture)
        {
            Headshots.texture = texture;
        }

        void Unassign()
        {
            if (BuildingToTrack == null)
                return;

            ScavengerTeamModel assignedModel = ScavengerTeamController.GetTeamAssignedToCityBuilding(BuildingToTrack);

            if (assignedModel == null || !assignedModel.HasMembersAssigned())
                return;

            assignedModel.AssignToBuilding(null);
        }
    }
}