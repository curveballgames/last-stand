﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curveball;
using UnityEngine.UI;
using TMPro;

namespace LastStand
{
    public class DayReportUIController : CBGUIComponent
    {
        public CanvasGroupFader CanvasFader;
        public TextMeshProUGUI Title;
        public Transform SurvivorPanelParent;
        public Button ContinueButton;
        public TextMeshProUGUI ContinueButtonText;

        private static List<SurvivorReportPanel> SurvivorPanels;

        private void Awake()
        {
            SurvivorPanels = new List<SurvivorReportPanel>();

            EventSystem.Subscribe<ShowReportEvent>(OnShowReport, this);

            ContinueButton.onClick.AddListener(OnContinueClicked);
        }

        void OnShowReport(ShowReportEvent e)
        {
            int counter = 0;

            foreach (SurvivorModel model in SurvivorModel.AllModels)
            {
                if (SurvivorPanels.Count <= counter)
                {
                    CreateNewSurvivorPanel();
                }

                SurvivorPanels[counter].ConfigureForSurvivor(model);
                SurvivorPanels[counter].SetActive(true);
                counter++;
            }

            for (; counter < SurvivorPanels.Count; counter++)
            {
                SurvivorPanels[counter].SetActive(false);
            }

            ContinueButtonText.text = "TODO";
            Title.text = "TODO";

            CanvasFader.ForceShow();
        }

        void CreateNewSurvivorPanel()
        {
            GameObject newSurvivorPanel = Instantiate(PrefabDictionary.Singleton.SurvivorReportPanelPrefab.gameObject, SurvivorPanelParent);
            SurvivorPanels.Add(newSurvivorPanel.GetComponent<SurvivorReportPanel>());
        }

        void OnContinueClicked()
        {

        }
    }
}