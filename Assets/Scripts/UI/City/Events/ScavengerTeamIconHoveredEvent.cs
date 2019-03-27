using Curveball;

namespace LastStand
{
    public struct ScavengerTeamIconHoveredEvent : IEvent
    {
        public ScavengerTeamAssignmentIcon Icon;

        public ScavengerTeamIconHoveredEvent(ScavengerTeamAssignmentIcon icon)
        {
            Icon = icon;
        }
    }
}