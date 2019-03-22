using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curveball;
using Curveball.Strategy;
using TMPro;

namespace LastStand
{
    public class SurvivorInfoPanel : CBGGameObject
    {
        public CanvasGroupFader Fader;
        public TextMeshProUGUI SurvivorName;
        public SurvivorStatBar ShootingBar;
        public SurvivorStatBar FitnessBar;
        public SurvivorStatBar StrengthBar;

        private SurvivorModel currentModel;

        private void Awake()
        {
            EventSystem.Subscribe<SurvivorIconHoveredEvent>(OnSurvivorIconHovered, this);
            EventSystem.Subscribe<SurvivorIconUnhoveredEvent>(OnSurvivorIconUnhovered, this);
        }

        void OnSurvivorIconHovered(SurvivorIconHoveredEvent e)
        {
            if (e.Icon.Model != currentModel)
            {
                ConfigureAndShowForModel(e.Icon.Model);
            }
        }

        void OnSurvivorIconUnhovered(SurvivorIconUnhoveredEvent e)
        {
            if (e.Icon.Model == currentModel)
            {
                currentModel = null;
                Hide(false);
            }
        }

        void ConfigureAndShowForModel(SurvivorModel model)
        {
            currentModel = model;

            SurvivorName.text = model.Name;
            ShootingBar.ConfigureForModel(model, model.ShootingSkill);
            FitnessBar.ConfigureForModel(model, model.FitnessSkill);
            StrengthBar.ConfigureForModel(model, model.StrengthSkill);

            Fader.FadeIn();
        }

        void Hide(bool immediate)
        {
            Fader.FadeOut();
        }
    }
}