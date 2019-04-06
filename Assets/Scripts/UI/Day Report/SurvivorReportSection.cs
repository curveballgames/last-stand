using System.Collections.Generic;
using UnityEngine;
using Curveball;

namespace LastStand
{
    public class SurvivorReportSection : CBGUIComponent
    {
        private static List<SurvivorReportPanel> Panels = new List<SurvivorReportPanel>();

        public Transform PanelParent;

        public void UpdateView()
        {
            int counter = 0;
            foreach (SurvivorModel model in SurvivorModel.AllModels)
            {
                if (Panels.Count <= counter)
                {
                    CreateNewSurvivorPanel();
                }

                Panels[counter].ConfigureForSurvivor(model);
                Panels[counter].SetActive(true);
                counter++;
            }

            for (; counter < Panels.Count; counter++)
            {
                Panels[counter].SetActive(false);
            }
        }

        void CreateNewSurvivorPanel()
        {
            GameObject newSurvivorPanel = Instantiate(PrefabDictionary.Singleton.SurvivorReportPanelPrefab.gameObject, PanelParent);
            Panels.Add(newSurvivorPanel.GetComponent<SurvivorReportPanel>());
        }
    }
}