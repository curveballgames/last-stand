using Curveball;

namespace LastStand
{
    public class SurvivorIconClickedEvent : IEvent
    {
        public SurvivorModel Model;

        public SurvivorIconClickedEvent(SurvivorModel model)
        {
            Model = model;
        }
    }
}