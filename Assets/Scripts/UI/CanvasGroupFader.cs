using UnityEngine;
using Curveball;
using DG.Tweening;

namespace LastStand
{
    [RequireComponent(typeof(CanvasGroup))]
    [ExecuteInEditMode]
    public class CanvasGroupFader : CBGUIComponent
    {
        public CanvasGroup Group;
        public bool StartFadedOut = true;
        public float FadeTime = 0.1f;

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
            Group.DOFade(1f, FadeTime);
        }

        public void FadeOut()
        {
            ToggleInteractability(false);
            Group.DOFade(0f, FadeTime);
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