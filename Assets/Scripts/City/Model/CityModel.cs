using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curveball;

namespace LastStand
{
    public class CityModel : CBGGameObject
    {
        public static CityModel CurrentCity;

        public string Name;
        public CityBuildingModel[] BuildingModels;
        public CityBuildingSpawnSlot[] SpawnSlots;

        public void InitialiseNew()
        {
            foreach (CityBuildingSpawnSlot spawnSlot in SpawnSlots)
            {
                CityBuildingModel model = Instantiate(PrefabDictionary.GetRandomBuildingModelForSpawnSlot(spawnSlot).gameObject, transform).GetComponent<CityBuildingModel>();
                model.RandomizeProperties();
                model.RandomizeDanger();

                model.transform.position = model.Location = spawnSlot.transform.position;
                model.transform.rotation = model.Rotation = spawnSlot.transform.rotation;
            }
        }

        public void CopyFrom(CityModel other)
        {
            for (int i = 0; i < BuildingModels.Length; i++)
            {
                BuildingModels[i].CopyFrom(other.BuildingModels[i]);
            }
        }
    }
}