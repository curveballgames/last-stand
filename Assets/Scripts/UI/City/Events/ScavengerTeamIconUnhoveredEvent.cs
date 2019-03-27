using Curveball;

namespace LastStand
{
    public struct ScavengerTeamIconUnhoveredEvent : IEvent
    {
        public ScavengerTeamAssignmentIcon Icon;

        public ScavengerTeamIconUnhoveredEvent(ScavengerTeamAssignmentIcon icon)
        {
            Icon = icon;
        }
    }
}