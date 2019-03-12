using UnityEngine;

namespace LastStand
{
    [System.Serializable]
    public class SaveGameMetadata
    {
        public long SaveTimestamp;
        public string BaseName;

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }

        public static SaveGameMetadata FromJSON(string json)
        {
            return JsonUtility.FromJson<SaveGameMetadata>(json);
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(BaseName);
        }
    }
}