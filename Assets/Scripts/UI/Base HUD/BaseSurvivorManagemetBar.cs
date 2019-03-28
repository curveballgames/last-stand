﻿using System.Collections.Generic;
using UnityEngine;
using Curveball;

namespace LastStand
{
    public class BaseSurvivorManagemetBar : CBGUIComponent
    {
        public Transform IconParent;

        private static List<BaseSurvivorAssignIcon> createdIcons = new List<BaseSurvivorAssignIcon>();

        private void Awake()
        {
            EventSystem.Subscribe<SurvivorAssignmentUpdatedEvent>(OnSurvivorAssignmentUpdated, this);
        }

        private void OnEnable()
        {
            for (int i = 0; i < SurvivorModel.AllModels.Count; i++)
            {
                if (createdIcons.Count < SurvivorModel.AllModels.Count)
                {
                    CreateNewSurvivorIcon();
                }

                BaseSurvivorAssignIcon icon = createdIcons[i];
                icon.Model = SurvivorModel.AllModels[i];
                icon.CreateHeadshot();
                icon.UpdateView();
                icon.SetActive(true);
            }

            if (SurvivorModel.AllModels.Count < createdIcons.Count)
            {
                for (int i = SurvivorModel.AllModels.Count; i < createdIcons.Count; i++)
                {
                    createdIcons[i].SetActive(false);
                }
            }
        }

        void CreateNewSurvivorIcon()
        {
            GameObject newIcon = Instantiate(PrefabDictionary.Singleton.SurvivorIconPrefab.gameObject, IconParent);
            createdIcons.Add(newIcon.GetComponent<BaseSurvivorAssignIcon>());
            newIcon.SetActive(false);
        }

        void OnSurvivorAssignmentUpdated(SurvivorAssignmentUpdatedEvent e)
        {
            foreach (BaseSurvivorAssignIcon icon in createdIcons)
            {
                if (icon.gameObject.activeSelf && icon.Model == e.SurvivorModel)
                {
                    icon.UpdateView();
                    return;
                }
            }
        }
    }
}