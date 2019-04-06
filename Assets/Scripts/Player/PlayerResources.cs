using UnityEngine;
using Curveball;

namespace LastStand
{
    public class PlayerResources : CBGGameObject
    {
        private const float MATERIAL_REFUND_MULTIPLIER = 0.75f;

        private const int STARTING_MATERIALS = 6;
        private const int STARTING_FOOD = 5;

        public static PlayerResources Singleton;

        [SerializeField]
        private int buildingMaterials;
        [SerializeField]
        private int food;

        public int BuildingMaterials { get => buildingMaterials; }
        public int Food { get => food; }

        private void Awake()
        {
            Singleton = this;

            EventSystem.Subscribe<StartRoomConstructionEvent>(OnConstructionStarted, this);
            EventSystem.Subscribe<NewGameEvent>(OnNewGame, this);
            EventSystem.Subscribe<RoomConstructionCancelledEvent>(OnConstructionCancelled, this);
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

        void OnConstructionCancelled(RoomConstructionCancelledEvent e)
        {
            if (e.Model.IsBuilt)
            {
                buildingMaterials += Mathf.CeilToInt(RoomTypeDictionary.Costs[e.Model.RoomType] * MATERIAL_REFUND_MULTIPLIER);
            }
            else
            {
                buildingMaterials += RoomTypeDictionary.Costs[e.Model.RoomType];
            }

            EventSystem.Publish(new PlayerResourcesUpdatedEvent());
        }

        public void AddResources(CityBuildingModel.ResourceBundle resources)
        {
            if (resources.Food == 0 && resources.BuildingMaterials == 0)
                return;

            food += resources.Food;
            buildingMaterials += resources.BuildingMaterials;

            EventSystem.Publish(new PlayerResourcesUpdatedEvent());
        }
    }
}