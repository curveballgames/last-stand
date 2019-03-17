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

        private void Awake()
        {
            EventSystem.Subscribe<RoomModelUpdatedEvent>(OnRoomModelUpdated, this);

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

        void OnModelsInitialised(ModelsInitialisedEvent e)
        {
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

        }

        void ConfigureForEmptyRoom()
        {
            if (buildButton == null)
            {
                buildButton = Instantiate(PrefabDictionary.Singleton.RoomBuildButtonPrefab, UIManager.Singleton.BuildButtonParent);
            }

            buildButton.LinkedRoom = this;
            buildButton.SetActive(true);
        }

        void ConfigureForRoomUnderConstruction()
        {
            buildButton.SetActive(false);
        }
    }
}