using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curveball;

namespace LastStand
{
    public class CityViewController : CBGGameObject
    {
        public Transform CityRoot;

        private GameObject cityGameObject;

        private void Awake()
        {
            EventSystem.Subscribe<ModelsInitialisedEvent>(OnModelsInitialised, this);
            EventSystem.Subscribe<ShowDayOverviewEvent>(OnShowDayOverview, this);

            CityRoot.gameObject.SetActive(false);
        }

        void OnModelsInitialised(ModelsInitialisedEvent e)
        {
            DestroyView();

            CityModel currentCity = CityModel.CurrentCity;

            GameObject basePrefab = PrefabDictionary.GetCityWithName(currentCity.Name).gameObject;
            cityGameObject = Instantiate(basePrefab, CityRoot);

            if (!e.FromSaveFile)
            {
                cityGameObject.GetComponent<CityModel>().InitialiseNew();
            }
            else
            {
                cityGameObject.GetComponent<CityModel>().CopyFrom(currentCity);
            }

            CityModel.CurrentCity = cityGameObject.GetComponent<CityModel>();
        }

        void OnShowDayOverview(ShowDayOverviewEvent e)
        {
            CityRoot.gameObject.SetActive(e.Type == DayOverviewType.City);
        }

        void DestroyView()
        {
            if (cityGameObject != null)
            {
                DestroyImmediate(cityGameObject);
                cityGameObject = null;
            }
        }
    }
}