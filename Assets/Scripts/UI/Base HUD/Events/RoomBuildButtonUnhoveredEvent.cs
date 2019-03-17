using Curveball;

namespace LastStand
{
    public struct RoomBuildButtonUnhoveredEvent : IEvent
    {
        public RoomBuildSelectButton SelectButton;

        public RoomBuildButtonUnhoveredEvent(RoomBuildSelectButton selectButton)
        {
            SelectButton = selectButton;
        }
    }
}