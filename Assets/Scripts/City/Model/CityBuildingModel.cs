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
        private ResourceBundle[] generationSteps;

        public void RandomizeProperties()
        {
            generationSteps = new ResourceBundle[RequiredExploreStages];

            int food = Random.Range(MinFood, MaxFood + 1);
            int materials = Random.Range(MinBuildingMaterials, MaxBuildingMaterials + 1);

            do
            {
                int randomIndex = Random.Range(0, generationSteps.Length);

                if (food > 0)
                {
                    generationSteps[randomIndex].Food++;
                    food--;
                }
                else if (materials > 0)
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

        public int GetFoodRemaining()
        {
            int food = 0;

            for (int i = StagesExplored; i < generationSteps.Length; i++)
            {
                food += generationSteps[i].Food;
            }

            return food;
        }

        public int GetBuildingMaterialsRemaining()
        {
            int materials = 0;

            for (int i = StagesExplored; i < generationSteps.Length; i++)
            {
                materials += generationSteps[i].BuildingMaterials;
            }

            return materials;
        }

        public int GetDangerLevel()
        {
            int dangerLevel = DangerLevel;

            ScavengerTeamModel assignedModel = ScavengerTeamController.GetTeamAssignedToCityBuilding(this);

            if (assignedModel == null || !assignedModel.HasMembersAssigned())
            {
                return dangerLevel;
            }

            HashSet<SurvivorModel> scavengers = assignedModel.LinkedRoom.AssignedSurvivors;
            foreach (SurvivorModel survivor in scavengers)
            {
                dangerLevel -= survivor.GetLevel(survivor.ShootingSkill);
            }

            dangerLevel = Mathf.Max(dangerLevel, 0);
            return dangerLevel;
        }

        public ResourceBundle Explore(ScavengerTeamModel scavengerTeam)
        {
            ResourceBundle resourcesScavenged = new ResourceBundle();

            for (int i = 0; i < scavengerTeam.LinkedRoom.AssignedSurvivors.Count; i++)
            {
                ResourceBundle genStep = generationSteps[StagesExplored];
                resourcesScavenged.BuildingMaterials += genStep.BuildingMaterials;
                resourcesScavenged.Food += genStep.Food;

                StagesExplored++;

                if (IsExplored)
                    break;
            }

            return resourcesScavenged;
        }

        public struct ResourceBundle
        {
            public int Food;
            public int BuildingMaterials;
        }
    }
}