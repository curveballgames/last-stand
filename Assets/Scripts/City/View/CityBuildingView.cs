using UnityEngine;
using Curveball;
using cakeslice;

namespace LastStand
{
    [RequireComponent(typeof(CityBuildingModel))]
    public class CityBuildingView : CBGGameObject
    {
        public CityBuildingModel LinkedModel;
        public Outline Outline;

        public Transform AssignedSurvivorsParent { get; private set; }

        private void Awake()
        {
            LinkedModel = GetComponent<CityBuildingModel>();

            AssignedSurvivorsParent = Instantiate(PrefabDictionary.Singleton.SurvivorAssignParentPrefab, UIManager.Singleton.SurvivorAssignParent).transform;
            AssignedSurvivorsParent.GetComponent<SurvivorAssignmentParent>().CityBuilding = this;

            EventSystem.Subscribe<CityBuildingHoverEvent>(OnCityBuildingHovered, this);

            UpdateOutlineColor(false);
        }

        private void OnDestroy()
        {
            if (AssignedSurvivorsParent != null)
            {
                DestroyImmediate(AssignedSurvivorsParent.gameObject);
            }

            EventSystem.Unsubscribe<CityBuildingHoverEvent>(OnCityBuildingHovered, this);
        }

        void OnCityBuildingHovered(CityBuildingHoverEvent e)
        {
            UpdateOutlineColor(e.Building == LinkedModel);
        }

        void UpdateOutlineColor(bool hovered)
        {
            Outline.color = hovered ? 1 : 0;
        }
    }
}