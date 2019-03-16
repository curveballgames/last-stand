using Curveball;

namespace LastStand
{
    public class SurvivorIconHoveredEvent : IEvent
    {
        public SurvivorModel Model;

        public SurvivorIconHoveredEvent(SurvivorModel model)
        {
            Model = model;
        }
    }
}