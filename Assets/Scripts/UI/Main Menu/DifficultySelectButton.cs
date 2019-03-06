using UnityEngine.UI;
using Curveball;

namespace LastStand
{
    public class DifficultySelectButton : CBGGameObject
    {
        public Button Button;
        public DifficultyLevel DifficultyLevel;

        private void Awake()
        {
            Button.onClick.RemoveAllListeners();
            Button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Curveball.EventSystem.Publish(new NewGameEvent(DifficultyLevel));
        }
    }
}