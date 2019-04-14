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
            
            EventSystem.Subscribe<NewGameEvent>(OnNewGame, this);
        }

        void OnNewGame(NewGameEvent e)
        {
            buildingMaterials = STARTING_MATERIALS;
            food = STARTING_FOOD;
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