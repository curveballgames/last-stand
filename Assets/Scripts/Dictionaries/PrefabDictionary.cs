using UnityEngine;
using Curveball;

namespace LastStand
{
    public class PrefabDictionary : CBGGameObject
    {
        public static PrefabDictionary Singleton;

        public BaseModel[] Bases;

        public RoomBuildButton RoomBuildButtonPrefab;
        public BaseSurvivorAssignIcon SurvivorIconPrefab;

        private void Awake()
        {
            Singleton = this;
        }

        public static BaseModel GetBaseWithName(string name)
        {
            foreach (BaseModel model in Singleton.Bases)
            {
                if (model.Name == name)
                {
                    return model;
                }
            }

            Debug.LogError("Could not find base with name " + name);
            return null;
        }
    }
}