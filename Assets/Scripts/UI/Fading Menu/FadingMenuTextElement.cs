using UnityEngine;
using TMPro;
using DG.Tweening;

namespace LastStand
{
    [System.Serializable]
    public class FadingMenuTextElement
    {
        public const float FADE_TIME = 0.425f;
        private static readonly Vector2 MOVE_OFFSET = new Vector3(-35f, 0f);

        public TextMeshProUGUI TextInstance;

        private Vector2 startPosition;
        private Vector2 fromPosition;
        private Color startColor;

        private bool initialised;

        public void FadeIn()
        {
            Initialise();

            TextInstance.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
            TextInstance.rectTransform.anchoredPosition = fromPosition;

            TextInstance.DOFade(1f, FADE_TIME).SetDelay(FADE_TIME);
            TextInstance.rectTransform.DOAnchorPos(startPosition, FADE_TIME).SetDelay(FADE_TIME);
        }

        public void FadeOut()
        {
            Initialise();

            TextInstance.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
            TextInstance.rectTransform.anchoredPosition = startPosition;

            TextInstance.DOFade(0f, FADE_TIME);
            TextInstance.rectTransform.DOAnchorPos(fromPosition, FADE_TIME);
        }

        private void Initialise()
        {
            if (initialised)
                return;

            startPosition = TextInstance.rectTransform.anchoredPosition;
            fromPosition = startPosition + MOVE_OFFSET;
            startColor = TextInstance.color;

            initialised = true;
        }
    }
}