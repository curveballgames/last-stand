using Curveball;

namespace LastStand
{
    public struct BuildButtonPressedEvent : IEvent
    {
        public RoomModel LinkedModel;

        public BuildButtonPressedEvent(RoomModel linkedModel)
        {
            this.LinkedModel = linkedModel;
        }
    }
}