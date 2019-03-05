using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Curveball;
using TMPro;
using DG.Tweening;

namespace LastStand
{
    public class FadingMenu : MenuScreen
    {
        public bool IsFirstScreen;
        public FadingMenuTextElement[] TextInstances;
        public Button[] Buttons;

        private void Start()
        {
            if (IsFirstScreen)
            {
                MenuSystem.OpenMenu(this, true);
            }
        }

        public override void Open(bool instant)
        {
            DisableButtons();
            SetActive(true);

            foreach (FadingMenuTextElement textInstance in TextInstances)
            {
                textInstance.FadeIn();
            }

            Timer.CreateTimer(gameObject, FadingMenuTextElement.FADE_TIME + 0.1f, EnableButtons);
        }

        public override void Close(bool instant, MenuScreen toOpen = null)
        {
            DisableButtons();

            foreach (FadingMenuTextElement textInstance in TextInstances)
            {
                textInstance.FadeOut();
            }

            Timer.CreateTimer(gameObject, FadingMenuTextElement.FADE_TIME, () =>
            {
                SetActive(false);
            });
        }

        private void EnableButtons()
        {
            foreach (Button b in Buttons)
            {
                b.interactable = true;
            }
        }

        private void DisableButtons()
        {
            foreach (Button b in Buttons)
            {
                b.interactable = false;
            }
        }
    }
}