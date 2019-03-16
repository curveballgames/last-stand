using UnityEngine;
using UnityEngine.UI;
using Curveball;
using DG.Tweening;
using UnityEngine.Events;

namespace LastStand
{
    public class ScreenFadeController : CBGUIComponent
    {
        private const float FADE_TIME = 0.5f;

        public Image FadeImage;

        private Color fromColor;
        private Color toColor;

        private void Awake()
        {
            fromColor = new Color(0.05f, 0.05f, 0.05f, 0f);
            toColor = new Color(0.05f, 0.05f, 0.05f, 1f);

            EventSystem.Subscribe<StartNewGameFadeEvent>(OnNewGameFadeStart, this);
            EventSystem.Subscribe<ViewInitialisedEvent>(OnViewInitialised, this);

            Hide();
        }

        void OnNewGameFadeStart(StartNewGameFadeEvent e)
        {
            FadeToOpaque(() =>
            {
                EventSystem.Publish(new NewGameEvent(e.DifficultyLevel));
            });
        }

        void OnViewInitialised(ViewInitialisedEvent e)
        {
            FadeToTransparent();
        }

        void FadeToOpaque(UnityAction onCompleteCallback = null)
        {
            FadeImage.color = fromColor;
            FadeImage.enabled = true;

            Tweener t = FadeImage.DOFade(1f, FADE_TIME);

            if (onCompleteCallback != null)
            {
                t.OnComplete(() => {
                    Timer.CreateTimer(gameObject, 0.1f, onCompleteCallback);
                });
            }
        }

        void FadeToTransparent()
        {
            FadeImage.color = toColor;
            FadeImage.enabled = true;

            FadeImage.DOFade(0f, FADE_TIME).OnComplete(Hide);
        }

        void Hide()
        {
            FadeImage.enabled = false;
        }
    }
}