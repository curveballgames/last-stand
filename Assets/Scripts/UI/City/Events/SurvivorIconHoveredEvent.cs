using Curveball;

namespace LastStand
{
    public struct SurvivorIconHoveredEvent : IEvent
    {
        public SurvivorAssignIcon Icon;

        public SurvivorIconHoveredEvent(SurvivorAssignIcon icon)
        {
            Icon = icon;
        }
    }
}