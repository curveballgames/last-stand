using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curveball;

namespace LastStand
{
    public class RoomView : CBGGameObject
    {
        public RoomModel LinkedModel;

        private RoomBuildButton buildButton;
        private RoomAssignmentSlots assignmentSlots;

        private void Awake()
        {
            EventSystem.Subscribe<RoomModelUpdatedEvent>(OnRoomModelUpdated, this);

            buildButton = Instantiate(PrefabDictionary.Singleton.RoomBuildButtonPrefab, UIManager.Singleton.BuildButtonParent);
            assignmentSlots = Instantiate(PrefabDictionary.Singleton.RoomAssignmentSlotsPrefab, UIManager.Singleton.RoomAssignmentSlotsParent);
            assignmentSlots.SetLinkedRoom(this);

            UpdateView();
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<RoomModelUpdatedEvent>(OnRoomModelUpdated, this);
        }

        void OnRoomModelUpdated(RoomModelUpdatedEvent e)
        {
            if (e.Room != LinkedModel)
                return;

            UpdateView();
        }

        void UpdateView()
        {
            if (LinkedModel.IsBuilt)
            {
                ConfigureForBuiltRoom();
            }
            else if (!LinkedModel.IsBuilt && LinkedModel.RoomType == RoomType.Empty)
            {
                ConfigureForEmptyRoom();
            }
            else if (!LinkedModel.IsBuilt && LinkedModel.RoomType != RoomType.Empty)
            {
                ConfigureForRoomUnderConstruction();
            }
        }

        void ConfigureForBuiltRoom()
        {
            buildButton.SetActive(false);
            assignmentSlots.UpdateView(true);
            assignmentSlots.SetActive(true);
        }

        void ConfigureForEmptyRoom()
        {
            buildButton.LinkedRoom = this;
            buildButton.SetActive(true);
            assignmentSlots.SetActive(false);
        }

        void ConfigureForRoomUnderConstruction()
        {
            buildButton.SetActive(false);
            assignmentSlots.UpdateView(false);
            assignmentSlots.SetActive(true);
        }
    }
}