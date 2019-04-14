using Curveball;

namespace LastStand
{
    public struct ZoomToTargetEvent : IEvent
    {
        public SurvivorModel Survivor;

        public ZoomToTargetEvent(SurvivorModel survivor)
        {
            Survivor = survivor;
        }
    }
}