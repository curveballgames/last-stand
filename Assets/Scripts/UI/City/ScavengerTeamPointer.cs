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
    }
}