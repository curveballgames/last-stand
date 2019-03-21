using Curveball;
using System.Collections.Generic;
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

        public HashSet<SurvivorModel> AssignedSurvivors { get => assignedSurvivors; }
        private HashSet<SurvivorModel> assignedSurvivors;

        private void Awake()
        {
            assignedSurvivors = new HashSet<SurvivorModel>();

            EventSystem.Subscribe<StartRoomConstructionEvent>(OnConstructionStarted, this);
            EventSystem.Subscribe<SurvivorAssignmentUpdatedEvent>(OnSurvivorAssignmentUpdated, this);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<StartRoomConstructionEvent>(OnConstructionStarted, this);
            EventSystem.Unsubscribe<SurvivorAssignmentUpdatedEvent>(OnSurvivorAssignmentUpdated, this);
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

        void OnSurvivorAssignmentUpdated(SurvivorAssignmentUpdatedEvent e)
        {
            if (e.Added && e.RoomModel == this)
                assignedSurvivors.Add(e.SurvivorModel);
            else assignedSurvivors.Remove(e.SurvivorModel);

            EventSystem.Publish(new RoomModelUpdatedEvent(this));
        }

        public void CopyFrom(RoomModel other)
        {
            BuildProgress = other.BuildProgress;
            RoomType = other.RoomType;
            IsBuilt = other.IsBuilt;
        }

        public bool IsFull()
        {
            return AssignedSurvivors.Count == AssignmentSlots;
        }
    }
}