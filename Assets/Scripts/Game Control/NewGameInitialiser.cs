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
            BaseModel.CurrentBase = PrefabDictionary.GetBaseWithName(STARTING_BASE_NAME);
            CityModel.CurrentCity = PrefabDictionary.GetRandomStartCity();

            SurvivorModel.Initialise();
            SurvivorGenerator.GenerateInitialSurvivors();

            EventSystem.Publish(new ModelsInitialisedEvent(false));
        }
    }
}