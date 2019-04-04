using Curveball;

namespace LastStand
{
    public class GameStateController : CBGGameObject
    {
        public static GameState CurrentState { get; private set; }

        private void Awake()
        {
            EventSystem.Subscribe<NewGameEvent>(OnNewGame, this);
            EventSystem.Subscribe<AdvanceDayEvent>(OnAdvanceDay, this);
        }

        void OnNewGame(NewGameEvent e)
        {
            CurrentState = GameState.Morning;
        }

        void OnAdvanceDay(AdvanceDayEvent e)
        {
            switch (CurrentState)
            {
                case GameState.Morning:
                    CurrentState = GameState.Afternoon;
                    break;
                case GameState.Afternoon:
                    CurrentState = GameState.Evening;
                    break;
                case GameState.Evening:
                    CurrentState = GameState.Night;
                    break;
                case GameState.Night:
                    CurrentState = GameState.Morning;
                    break;
            }
        }
    }
}