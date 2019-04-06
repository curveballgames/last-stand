using System.Collections.Generic;
using UnityEngine;
using Curveball;

namespace LastStand
{
    public class ScavengerReportSection : CBGUIComponent
    {
        private static List<ScavengerReportPanel> Panels = new List<ScavengerReportPanel>();

        public Transform PanelParent;

        public void UpdateView()
        {
            int counter = 0;
            foreach (ScavengerTeamModel model in ScavengerTeamController.ScavengerTeams)
            {
                if (Panels.Count <= counter)
                {
                    CreateNewPanel();
                }

                Panels[counter].ConfigureForTeam(model);
                Panels[counter].SetActive(true);
                counter++;
            }

            for (; counter < Panels.Count; counter++)
            {
                Panels[counter].SetActive(false);
            }
        }

        void CreateNewPanel()
        {
            GameObject newScavengerPanel = Instantiate(PrefabDictionary.Singleton.ScavengerReportPanelPrefab.gameObject, PanelParent);
            Panels.Add(newScavengerPanel.GetComponent<ScavengerReportPanel>());
        }
    }
}