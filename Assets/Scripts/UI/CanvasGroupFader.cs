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
            }
            else
            {
                Group.alpha = 1f;
            }
        }

        public void FadeIn()
        {
            Group.DOFade(1f, FADE_TIME);
        }

        public void FadeOut()
        {
            Group.DOFade(0f, FADE_TIME);
        }
    }
}