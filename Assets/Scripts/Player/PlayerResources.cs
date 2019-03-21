using UnityEngine;
using Curveball;

namespace LastStand
{
    public class PlayerResources : CBGGameObject
    {
        private const int STARTING_MATERIALS = 6;
        private const int STARTING_FOOD = 5;

        public static PlayerResources Singleton;

        [SerializeField]
        private int buildingMaterials;
        [SerializeField]
        private int food;

        public int BuildingMaterials { get => buildingMaterials; }
        public int Food { get => food; set => food = value; }

        private void Awake()
        {
            Singleton = this;

            EventSystem.Subscribe<StartRoomConstructionEvent>(OnConstructionStarted, this);
            EventSystem.Subscribe<NewGameEvent>(OnNewGame, this);
        }

        void OnConstructionStarted(StartRoomConstructionEvent e)
        {
            buildingMaterials -= RoomTypeDictionary.Costs[e.TypeToBuild];
            EventSystem.Publish(new PlayerResourcesUpdatedEvent());
        }

        void OnNewGame(NewGameEvent e)
        {
            buildingMaterials = STARTING_MATERIALS;
            food = STARTING_FOOD;
            EventSystem.Publish(new PlayerResourcesUpdatedEvent());
        }
    }
}