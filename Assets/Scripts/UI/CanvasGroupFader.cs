using UnityEngine;
using Curveball;
using DG.Tweening;

namespace LastStand
{
    [RequireComponent(typeof(CanvasGroup))]
    [ExecuteInEditMode]
    public class CanvasGroupFader : CBGUIComponent
    {
        private const float FADE_TIME = 0.1f;

        public CanvasGroup Group;
        public bool StartFadedOut = true;

        private void Awake()
        {
            if (Group == null)
                Group = GetComponent<CanvasGroup>();

            if (StartFadedOut)
            {
                Group.alpha = 0f;
                ToggleInteractability(false);
            }
            else
            {
                Group.alpha = 1f;
                ToggleInteractability(true);
            }
        }

        public void FadeIn()
        {
            ToggleInteractability(true);
            Group.DOFade(1f, FADE_TIME);
        }

        public void FadeOut()
        {
            ToggleInteractability(false);
            Group.DOFade(0f, FADE_TIME);
        }

        void ToggleInteractability(bool interactable)
        {
            Group.interactable = interactable;
            Group.blocksRaycasts = interactable;
        }

        public void ForceShow()
        {
            Group.alpha = 1f;
            ToggleInteractability(true);
        }
    }
}