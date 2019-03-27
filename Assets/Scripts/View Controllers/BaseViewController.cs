using UnityEngine;
using Curveball;

namespace LastStand
{
    public class BaseViewController : CBGGameObject
    {
        public Transform BaseRoot;
        private GameObject baseGameObject;

        private void Awake()
        {
            EventSystem.Subscribe<ModelsInitialisedEvent>(OnModelsInitialised, this);
            EventSystem.Subscribe<ShowDayOverviewEvent>(OnShowDayOverview, this);
        }

        void OnModelsInitialised(ModelsInitialisedEvent e)
        {
            DestroyView();

            BaseModel currentBase = BaseModel.CurrentBase;

            GameObject basePrefab = PrefabDictionary.GetBaseWithName(currentBase.Name).gameObject;
            baseGameObject = Instantiate(basePrefab, BaseRoot);

            baseGameObject.GetComponent<BaseModel>().CopyFrom(currentBase);
            BaseModel.CurrentBase = baseGameObject.GetComponent<BaseModel>();

            ScavengerTeamController.CreateScavengerTeamsForCurrentBase();

            Timer.CreateTimer(gameObject, 0.1f, () =>
            {
                EventSystem.Publish(new ViewInitialisedEvent());
            });
        }

        void OnShowDayOverview(ShowDayOverviewEvent e)
        {
            BaseRoot.gameObject.SetActive(e.Type == DayOverviewType.Base);
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