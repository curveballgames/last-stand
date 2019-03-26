using System.Collections;
using System.Collections.Generic;
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

        private void Awake()
        {
            LinkedModel = GetComponent<CityBuildingModel>();
            EventSystem.Subscribe<CityBuildingHoverEvent>(OnCityBuildingHovered, this);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<CityBuildingHoverEvent>(OnCityBuildingHovered, this);
        }

        void OnCityBuildingHovered(CityBuildingHoverEvent e)
        {
            Outline.color = e.Building == LinkedModel ? 1 : 0;
        }
    }
}