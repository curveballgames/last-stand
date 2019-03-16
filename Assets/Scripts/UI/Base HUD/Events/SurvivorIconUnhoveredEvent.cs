using Curveball;

namespace LastStand
{
    public struct SurvivorIconUnhoveredEvent : IEvent
    {
        public BaseSurvivorAssignIcon Icon;

        public SurvivorIconUnhoveredEvent(BaseSurvivorAssignIcon icon)
        {
            Icon = icon;
        }
    }
}