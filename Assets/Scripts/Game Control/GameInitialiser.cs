using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curveball;

namespace LastStand
{
    public class GameInitialiser : CBGGameObject
    {
        private void Awake()
        {
            EventSystem.Subscribe<NewGameEvent>(OnNewGame, this);
        }

        private void OnNewGame(NewGameEvent e)
        {
            SurvivorModel.Initialise();
            SurvivorGenerator.GenerateInitialSurvivors();
        }
    }
}