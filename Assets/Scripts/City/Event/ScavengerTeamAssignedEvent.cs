using Curveball;

namespace LastStand
{
    public struct ScavengerTeamAssignedEvent : IEvent
    {
        public ScavengerTeamModel Model;

        public ScavengerTeamAssignedEvent(ScavengerTeamModel model)
        {
            Model = model;
        }
    }
}