using Curveball;

namespace LastStand
{
    public struct SwitchDayOverviewEvent : IEvent
    {
        public DayOverviewType Type;

        public SwitchDayOverviewEvent(DayOverviewType type)
        {
            Type = type;
        }
    }
}