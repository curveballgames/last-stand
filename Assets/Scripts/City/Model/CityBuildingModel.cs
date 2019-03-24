using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curveball;

namespace LastStand
{
    public class CityBuildingModel : CBGGameObject
    {
        public const int MIN_DANGER = 3;
        public const int MAX_DANGER = 10;

        public string Name;
        public int Width;
        public int Height;
        public CityBuildingType BuildingType;

        [Space]
        public int MinFood;
        public int MaxFood;
        public int MinBuildingMaterials;
        public int MaxBuildingMaterials;
        public int RequiredExploreStages = 3;

        [HideInInspector]
        public int StagesExplored;
        [HideInInspector]
        public int DangerLevel;
        [HideInInspector]
        public Vector3 Location;
        [HideInInspector]
        public Quaternion Rotation;

        public bool IsExplored { get => StagesExplored == RequiredExploreStages; }

        [SerializeField]
        private ResourceGenerationStep[] generationSteps;

        public void RandomizeProperties()
        {
            generationSteps = new ResourceGenerationStep[RequiredExploreStages];

            int food = Random.Range(MinFood, MaxFood + 1);
            int materials = Random.Range(MaxFood, MaxBuildingMaterials + 1);

            do
            {
                int randomIndex = Random.Range(0, generationSteps.Length);

                if (food > 0)
                {
                    generationSteps[randomIndex].Food++;
                    food--;
                }

                if (materials > 0)
                {
                    generationSteps[randomIndex].BuildingMaterials++;
                    materials--;
                }
            }
            while (food > 0 && materials > 0);
        }

        public void RandomizeDanger()
        {
            DangerLevel = Random.Range(MIN_DANGER, MAX_DANGER);
        }

        public void CopyFrom(CityBuildingModel other)
        {
            StagesExplored = other.StagesExplored;
            DangerLevel = other.DangerLevel;
            Location = other.Location;
            Rotation = other.Rotation;
            generationSteps = other.generationSteps;
        }

        public struct ResourceGenerationStep
        {
            public int Food;
            public int BuildingMaterials;
        }
    }
}