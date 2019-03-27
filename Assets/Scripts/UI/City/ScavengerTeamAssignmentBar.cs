using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curveball;

namespace LastStand
{
    public class ScavengerTeamAssignmentBar : CBGUIComponent
    {
        public Transform IconParent;

        private static List<ScavengerTeamAssignmentIcon> createdIcons = new List<ScavengerTeamAssignmentIcon>();

        private void Awake()
        {
            EventSystem.Subscribe<SurvivorAssignmentUpdatedEvent>(OnSurvivorAssignmentUpdated, this);
            EventSystem.Subscribe<ScavengerTeamAssignedEvent>(OnScavengerTeamAssigned, this);
        }

        private void OnEnable()
        {
            UpdateScavengerIcons();
        }

        void OnSurvivorAssignmentUpdated(SurvivorAssignmentUpdatedEvent e)
        {
            UpdateScavengerIcons();
        }

        void OnScavengerTeamAssigned(ScavengerTeamAssignedEvent e)
        {
            UpdateScavengerIcons();
        }

        void UpdateScavengerIcons()
        {
            if (ScavengerTeamController.ScavengerTeams == null)
                return;

            for (int i = 0; i < ScavengerTeamController.ScavengerTeams.Count; i++)
            {
                if (createdIcons.Count < SurvivorModel.AllModels.Count)
                {
                    CreateIcon();
                }

                ScavengerTeamAssignmentIcon icon = createdIcons[i];
                icon.Model = ScavengerTeamController.ScavengerTeams[i];
                icon.UpdateView();
                icon.SetActive(true);
            }

            if (ScavengerTeamController.ScavengerTeams.Count < createdIcons.Count)
            {
                for (int i = ScavengerTeamController.ScavengerTeams.Count; i < createdIcons.Count; i++)
                {
                    createdIcons[i].SetActive(false);
                }
            }
        }

        void CreateIcon()
        {
            GameObject newIcon = Instantiate(PrefabDictionary.Singleton.ScavengerTeamAssignmentIconPrefab.gameObject, IconParent);
            createdIcons.Add(newIcon.GetComponent<ScavengerTeamAssignmentIcon>());
            newIcon.SetActive(false);
        }
    }
}