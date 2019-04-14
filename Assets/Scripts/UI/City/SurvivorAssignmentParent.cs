using UnityEngine;
using Curveball;
using System.Collections.Generic;

namespace LastStand
{
    public class SurvivorAssignmentParent : CBGGameObject
    {
        private static readonly Vector3 BUILDING_OFFSET = new Vector3(0f, 4f, 0f);

        public CityBuildingView CityBuilding { get; set; }

        private GameObjectPool<SurvivorAssignmentPointer> pointerPool;

        private void Awake()
        {
            pointerPool = new GameObjectPool<SurvivorAssignmentPointer>();
            pointerPool.Prefab = PrefabDictionary.Singleton.ScavengerTeamAssignmentPointerPrefab;

            EventSystem.Subscribe<SurvivorAssignmentUpdatedEvent>(OnSurvivorAssignmentUpdated, this);
        }

        private void LateUpdate()
        {
            transform.position = Camera.main.WorldToScreenPoint(CityBuilding.transform.position + BUILDING_OFFSET);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<SurvivorAssignmentUpdatedEvent>(OnSurvivorAssignmentUpdated, this);
        }

        void OnSurvivorAssignmentUpdated(SurvivorAssignmentUpdatedEvent e)
        {
            List<SurvivorModel> assignedSurvivors = CityBuilding.LinkedModel.AssignedSurvivors;

            for (int i = 0; i < assignedSurvivors.Count; i++)
            {
                SurvivorAssignmentPointer sap = pointerPool.Get(i);
                sap.SetSurvivor(assignedSurvivors[i]);
                sap.transform.parent = transform;
                sap.SetActive(true);
            }

            pointerPool.ReturnAllAfter(CityBuilding.LinkedModel.AssignedSurvivors.Count);
        }
    }
}