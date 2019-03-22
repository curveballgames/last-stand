using Curveball;

namespace LastStand
{
    public struct RoomSelectEvent : IEvent
    {
        public RoomModel Model;

        public RoomSelectEvent(RoomModel model)
        {
            Model = model;
        }
    }
}