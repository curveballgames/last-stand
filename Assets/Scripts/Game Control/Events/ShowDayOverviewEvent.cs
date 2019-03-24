using Curveball;

namespace LastStand
{
    public struct ShowDayOverviewEvent : IEvent
    {
        public DayOverviewType Type;

        public ShowDayOverviewEvent(DayOverviewType type)
        {
            Type = type;
        }
    }
}