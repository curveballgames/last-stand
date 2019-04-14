using Curveball;
using System.Collections.Generic;

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
            List<CityBuildingModel> createdModels = new List<CityBuildingModel>();

            foreach (CityBuildingSpawnSlot spawnSlot in SpawnSlots)
            {
                CityBuildingModel model = Instantiate(PrefabDictionary.GetRandomBuildingModelForSpawnSlot(spawnSlot).gameObject, transform).GetComponent<CityBuildingModel>();
                model.RandomizeProperties();
                model.RandomizeDanger();

                model.transform.position = model.Location = spawnSlot.transform.position;
                model.transform.rotation = model.Rotation = spawnSlot.transform.rotation;

                createdModels.Add(model);
            }

            BuildingModels = createdModels.ToArray();
        }

        public void CopyFrom(CityModel other)
        {
            // TODO: this won't work

            for (int i = 0; i < BuildingModels.Length; i++)
            {
                BuildingModels[i].CopyFrom(other.BuildingModels[i]);
            }
        }
    }
}