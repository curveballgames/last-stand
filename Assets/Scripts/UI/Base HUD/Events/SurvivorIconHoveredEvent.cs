using Curveball;

namespace LastStand
{
    public struct SurvivorIconHoveredEvent : IEvent
    {
        public BaseSurvivorAssignIcon Icon;

        public SurvivorIconHoveredEvent(BaseSurvivorAssignIcon icon)
        {
            Icon = icon;
        }
    }
}