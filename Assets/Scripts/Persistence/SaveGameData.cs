using UnityEngine;

namespace LastStand
{
    [System.Serializable]
    public class SaveGameData
    {
        public CityBuildingModel CurrentBase;
        public SurvivorModel[] Survivors;

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }

        public static SaveGameData FromJSON(string json)
        {
            return JsonUtility.FromJson<SaveGameData>(json);
        }
    }
}