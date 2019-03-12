using UnityEngine;
using Curveball;

namespace LastStand
{
    public class PrefabDictionary : CBGGameObject
    {
        private static PrefabDictionary singleton;

        public BaseModel[] Bases;

        private void Awake()
        {
            singleton = this;
        }

        public static BaseModel GetBaseWithName(string name)
        {
            foreach (BaseModel model in singleton.Bases)
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