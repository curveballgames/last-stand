using UnityEngine.UI;
using Curveball;

namespace LastStand
{
    public class SurvivorStatBarStar : CBGUIComponent
    {
        public Image Fill;

        public void SetFilled()
        {
            Fill.enabled = true;
        }

        public void SetUnfilled()
        {
            Fill.enabled = false;
        }
    }
}