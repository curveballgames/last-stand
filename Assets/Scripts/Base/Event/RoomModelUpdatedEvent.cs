using Curveball;

namespace LastStand
{
    public struct RoomModelUpdatedEvent : IEvent
    {
        public RoomModel Room;

        public RoomModelUpdatedEvent(RoomModel room)
        {
            Room = room;
        }
    }
}