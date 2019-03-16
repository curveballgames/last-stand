using Curveball;
using UnityEngine;

namespace LastStand
{
    [RequireComponent(typeof(BoxCollider))]
    public class RoomModel : CBGGameObject
    {
        public int AssignmentSlots;
        public int BuildProgress;
        public RoomType RoomType;
        public bool IsBuilt;
        public bool IsOutside;

        public void CopyFrom(RoomModel other)
        {
            BuildProgress = other.BuildProgress;
            RoomType = other.RoomType;
            IsBuilt = other.IsBuilt;
        }
    }
}