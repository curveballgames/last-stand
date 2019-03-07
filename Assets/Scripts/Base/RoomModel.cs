using Curveball;
using UnityEngine;

namespace LastStand
{
    public class RoomModel : CBGGameObject
    {
        public int AssignmentSlots;
        public int BuildProgress;
        public RoomType RoomType;
        public bool IsBuilt;
        public bool IsOutside;
        public Vector3 Center;
    }
}