using Curveball;

namespace LastStand
{
    public struct SurvivorIconClickedEvent : IEvent
    {
        public BaseSurvivorAssignIcon Icon;

        public SurvivorIconClickedEvent(BaseSurvivorAssignIcon icon)
        {
            Icon = icon;
        }
    }
}