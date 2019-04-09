using System.Collections.Generic;
using UnityEngine;
using Curveball;

namespace LastStand
{
    public class ConstructionReportSection : CBGUIComponent
    {
        private static List<ConstructionReportPanel> Panels = new List<ConstructionReportPanel>();

        public Transform PanelParent;
        public GameObject NothingToReport;

        public void UpdateView()
        {
            bool modelsToShow = false;
            int counter = 0;

            foreach (RoomModel room in BaseModel.CurrentBase.Rooms)
            {
                if (room.BuildStepsCompleted > 0)
                {
                    AddPanel(room, counter);
                    modelsToShow = true;
                    counter++;
                }
            }

            for (; counter < Panels.Count; counter++)
            {
                Panels[counter].SetActive(false);
            }

            PanelParent.gameObject.SetActive(modelsToShow == true);
            NothingToReport.SetActive(modelsToShow == false);
        }

        void AddPanel(RoomModel room, int counter)
        {
            if (counter <= Panels.Count)
            {
                CreateNewPanel();
            }

            Panels[counter].ConfigureForRoom(room);
            Panels[counter].SetActive(true);
        }

        void CreateNewPanel()
        {
            GameObject newSurvivorPanel = Instantiate(PrefabDictionary.Singleton.ConstructionReportPanelPrefab.gameObject, PanelParent);
            Panels.Add(newSurvivorPanel.GetComponent<ConstructionReportPanel>());
        }
    }
}