using UnityEngine;
using Curveball;

namespace LastStand
{
    public class BaseViewController : CBGGameObject
    {
        private GameObject baseGameObject;

        private void Awake()
        {
            EventSystem.Subscribe<ModelsInitialisedEvent>(OnModelsInitialised, this);
        }

        void OnModelsInitialised(ModelsInitialisedEvent e)
        {
            DestroyView();

            BaseModel currentBase = BaseModel.CurrentBase;

            GameObject basePrefab = PrefabDictionary.GetBaseWithName(currentBase.Name).gameObject;
            baseGameObject = Instantiate(basePrefab);

            baseGameObject.GetComponent<BaseModel>().CopyFrom(currentBase);
            BaseModel.CurrentBase = baseGameObject.GetComponent<BaseModel>();

            EventSystem.Publish(new ViewInitialisedEvent());
        }

        void DestroyView()
        {
            if (baseGameObject != null)
            {
                DestroyImmediate(baseGameObject);
                baseGameObject = null;
            }
        }
    }
}