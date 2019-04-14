using Curveball;

namespace LastStand
{
    public struct SurvivorIconClickedEvent : IEvent
    {
        public SurvivorAssignIcon Icon;

        public SurvivorIconClickedEvent(SurvivorAssignIcon icon)
        {
            Icon = icon;
        }
    }
}