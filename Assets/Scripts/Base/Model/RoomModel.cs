﻿using Curveball;
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
        public bool Reclaimable;

        public HashSet<SurvivorModel> AssignedSurvivors { get => assignedSurvivors; }
        private HashSet<SurvivorModel> assignedSurvivors = new HashSet<SurvivorModel>();

        private void Awake()
        {
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

        public void CancelConstruction()
        {
            if (RoomType == RoomType.Empty)
                return;
            

            HashSet<SurvivorModel> assignmentClone = new HashSet<SurvivorModel>(assignedSurvivors);

            foreach (SurvivorModel model in assignmentClone)
            {
                model.AssignRoom(null);
            }

            EventSystem.Publish(new RoomConstructionCancelledEvent(this));

            IsBuilt = false;
            RoomType = RoomType.Empty;
            BuildProgress = 0;
            EventSystem.Publish(new RoomModelUpdatedEvent(this));
        }

        public void ContributeTowardsConstruction(SurvivorModel s)
        {
            if (IsBuilt)
            {
                return;
            }

            BuildProgress++;
            IsBuilt = BuildProgress >= RoomTypeDictionary.RoomBuildStages[RoomType];

            if (IsBuilt)
            {
                foreach (SurvivorModel assignee in AssignedSurvivors)
                {
                    assignee.AssignRoom(null);
                }
            }
        }
    }
}