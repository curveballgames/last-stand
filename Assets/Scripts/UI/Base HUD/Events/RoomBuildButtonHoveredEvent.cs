using Curveball;

namespace LastStand
{
    public struct RoomBuildButtonHoveredEvent : IEvent
    {
        public RoomBuildSelectButton SelectButton;

        public RoomBuildButtonHoveredEvent(RoomBuildSelectButton selectButton)
        {
            SelectButton = selectButton;
        }
    }
}