using Curveball;

namespace LastStand
{
    public class NewGameInitialiser : CBGGameObject
    {
        private static readonly string STARTING_BASE_NAME = "Suburban Base";

        private void Awake()
        {
            EventSystem.Subscribe<NewGameEvent>(OnNewGame, this);
            SurvivorNameGenerator.Initialise();
        }

        private void OnNewGame(NewGameEvent e)
        {
            SurvivorModel.Initialise();
            SurvivorGenerator.GenerateInitialSurvivors();

            BaseModel.CurrentBase = PrefabDictionary.GetBaseWithName(STARTING_BASE_NAME);

            EventSystem.Publish(new ModelsInitialisedEvent());
        }
    }
}