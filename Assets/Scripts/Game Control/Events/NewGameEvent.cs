using Curveball;

namespace LastStand
{
    public struct NewGameEvent : IEvent
    {
        public DifficultyLevel Difficulty;

        public NewGameEvent(DifficultyLevel difficulty)
        {
            Difficulty = difficulty;
        }
    }
}