using Curveball;

namespace LastStand
{
    public class SurvivorIconUnhoveredEvent : IEvent
    {
        public SurvivorModel Model;

        public SurvivorIconUnhoveredEvent(SurvivorModel model)
        {
            Model = model;
        }
    }
}