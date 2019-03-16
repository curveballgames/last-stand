using Curveball;

namespace LastStand
{
    public struct RoomHoverEvent : IEvent
    {
        public RoomModel Room;

        public RoomHoverEvent(RoomModel room)
        {
            Room = room;
        }
    }
}