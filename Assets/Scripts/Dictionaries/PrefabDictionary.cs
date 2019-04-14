using UnityEngine;
using Curveball;
using System.Collections.Generic;
using UnityEngine.UI;

namespace LastStand
{
    public class PrefabDictionary : CBGGameObject
    {
        public static PrefabDictionary Singleton;

        public CityModel[] Cities;
        public CityBuildingModel[] CityBuildings;

        [Space]
        public SurvivorAssignIcon SurvivorIconPrefab;
        public SurvivorAssignmentParent SurvivorAssignParentPrefab;
        [Space]
        public SurvivorAssignmentPointer ScavengerTeamAssignmentPointerPrefab;
        [Space]
        public Image ProgressBarFillPiece;
        [Space]
        public SurvivorReportPanel SurvivorReportPanelPrefab;

        private static Dictionary<CityBuildingType, List<CityBuildingModel>> cityBuildingsByType;

        private void Awake()
        {
            Singleton = this;
        }

        public static CityModel GetCityWithName(string name)
        {
            foreach (CityModel model in Singleton.Cities)
            {
                if (model.Name == name)
                {
                    return model;
                }
            }

            Debug.LogError("Could not find city with name " + name);
            return null;
        }

        public static CityModel GetRandomStartCity()
        {
            return Singleton.Cities[Random.Range(0, Singleton.Cities.Length)];
        }

        public static CityBuildingModel GetRandomBuildingModelForSpawnSlot(CityBuildingSpawnSlot spawnSlot)
        {
            InitialiseBuildingModelMap();

            List<CityBuildingModel> candidates = new List<CityBuildingModel>();

            foreach (CityBuildingType buildingType in spawnSlot.AcceptedTypes)
            {
                candidates.AddRange(cityBuildingsByType[buildingType]);
            }

            for (int i = candidates.Count - 1; i > -1; i--)
            {
                if (candidates[i].Width != spawnSlot.Width && candidates[i].Height != spawnSlot.Height)
                {
                    candidates.RemoveAt(i);
                }
            }

            if (candidates.Count == 0)
            {
                Debug.LogError("No candidates for spawn slot " + spawnSlot);
                return null;
            }

            return Utilities.SelectRandomlyFromList(candidates);
        }

        private static void InitialiseBuildingModelMap()
        {
            if (cityBuildingsByType != null)
                return;

            cityBuildingsByType = new Dictionary<CityBuildingType, List<CityBuildingModel>>();

            foreach (CityBuildingModel modelPrefab in Singleton.CityBuildings)
            {
                if (!cityBuildingsByType.ContainsKey(modelPrefab.BuildingType))
                {
                    cityBuildingsByType.Add(modelPrefab.BuildingType, new List<CityBuildingModel>());
                }

                cityBuildingsByType[modelPrefab.BuildingType].Add(modelPrefab);
            }
        }
    }
}