using UnityEngine;
using Curveball;

namespace LastStand
{
    public class ColorDictionary : CBGGameObject
    {
        public static ColorDictionary Singleton;

        public Color ScavengerTeamAssignedColor;
        public Color ScavengerTeamAvailableColor;
        public Color ScavengerTeamUnassignedColor;

        private void Awake()
        {
            Singleton = this;
        }
    }
}