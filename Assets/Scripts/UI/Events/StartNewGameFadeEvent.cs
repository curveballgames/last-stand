using Curveball;

namespace LastStand
{
    public class StartNewGameFadeEvent : IEvent
    {
        public DifficultyLevel DifficultyLevel;

        public StartNewGameFadeEvent(DifficultyLevel difficultyLevel)
        {
            DifficultyLevel = difficultyLevel;
        }
    }
}