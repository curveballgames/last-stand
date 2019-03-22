using Curveball;

namespace LastStand
{
    public struct RoomConstructionCancelledEvent : IEvent
    {
        public RoomModel Model;

        public RoomConstructionCancelledEvent(RoomModel model)
        {
            Model = model;
        }
    }
}