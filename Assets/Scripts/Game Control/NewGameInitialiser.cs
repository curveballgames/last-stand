using Curveball;

namespace LastStand
{
    public class NewGameInitialiser : CBGGameObject
    {
        private void Awake()
        {
            EventSystem.Subscribe<NewGameEvent>(OnNewGame, this);
            SurvivorNameGenerator.Initialise();
        }

        private void OnNewGame(NewGameEvent e)
        {
            CityModel.CurrentCity = PrefabDictionary.GetRandomStartCity();
            // TODO: setup base

            SurvivorModel.Initialise();
            SurvivorGenerator.GenerateInitialSurvivors();

            Timer.CreateTimer(gameObject, 0.05f, () =>
            {
                EventSystem.Publish(new ModelsInitialisedEvent(false));
            });
        }
    }
}