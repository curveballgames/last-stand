using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curveball;

namespace LastStand
{
    public class UIManager : CBGGameObject
    {
        private void Awake()
        {
            EventSystem.Subscribe<NewGameEvent>(OnNewGame, this);
        }

        private void OnNewGame(NewGameEvent e)
        {
            MenuSystem.CloseActiveMenu(false);
        }
    }
}