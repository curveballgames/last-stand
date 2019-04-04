using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curveball;

namespace LastStand
{
    public class GlobalLightController : CBGGameObject
    {
        public Light DirectionalLight;

        [Space]
        public Color MorningColor;
        public Vector3 MorningRotation;

        [Space]
        public Color AfternoonColor;
        public Vector3 AfternoonRotation;

        private void Awake()
        {
            EventSystem.Subscribe<DayPeriodUpdatedEvent>(OnDayPeriodUpdated, this);
        }

        void OnDayPeriodUpdated(DayPeriodUpdatedEvent e)
        {
            if (GameStateController.CurrentState == GameState.Morning)
            {
                DirectionalLight.color = MorningColor;
                DirectionalLight.transform.rotation = Quaternion.Euler(MorningRotation);
            }
            else if (GameStateController.CurrentState == GameState.Afternoon)
            {
                DirectionalLight.color = AfternoonColor;
                DirectionalLight.transform.rotation = Quaternion.Euler(AfternoonRotation);
            }
        }
    }
}