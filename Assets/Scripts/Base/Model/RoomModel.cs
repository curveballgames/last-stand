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

        private void Awake()
        {
            EventSystem.Subscribe<StartRoomConstructionEvent>(OnConstructionStarted, this);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<StartRoomConstructionEvent>(OnConstructionStarted, this);
        }

        void OnConstructionStarted(StartRoomConstructionEvent e)
        {
            if (e.LinkedModel == this)
            {
                BuildProgress = 0;
                RoomType = e.TypeToBuild;
                IsBuilt = false;
                EventSystem.Publish(new RoomModelUpdatedEvent(this));
            }
        }

        public void CopyFrom(RoomModel other)
        {
            BuildProgress = other.BuildProgress;
            RoomType = other.RoomType;
            IsBuilt = other.IsBuilt;
        }
    }
}