using Curveball;

namespace LastStand
{
    public struct SurvivorIconUnhoveredEvent : IEvent
    {
        public SurvivorAssignIcon Icon;

        public SurvivorIconUnhoveredEvent(SurvivorAssignIcon icon)
        {
            Icon = icon;
        }
    }
}