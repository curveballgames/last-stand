using Curveball;

namespace LastStand
{
    public struct StartRoomConstructionEvent : IEvent
    {
        public RoomType TypeToBuild;
        public RoomModel LinkedModel;

        public StartRoomConstructionEvent(RoomType typeToBuild, RoomModel linkedModel)
        {
            TypeToBuild = typeToBuild;
            LinkedModel = linkedModel;
        }
    }
}