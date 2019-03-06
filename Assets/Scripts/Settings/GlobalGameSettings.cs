using Curveball;

namespace LastStand
{
    public class GlobalGameSettings : CBGGameObject
    {
        public static DifficultyLevel DifficultyLevel { get; private set; }

        private void Awake()
        {
            EventSystem.Subscribe<NewGameEvent>(OnNewGame, this);
            DifficultyLevel = DifficultyLevel.Autumn;
        }

        private void OnNewGame(NewGameEvent e)
        {
            DifficultyLevel = e.Difficulty;
        }
    }
}