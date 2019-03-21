using Curveball;

namespace LastStand
{
    public struct SurvivorAssignmentUpdatedEvent : IEvent
    {
        public SurvivorModel SurvivorModel;
        public RoomModel RoomModel;
        public bool Added;

        public SurvivorAssignmentUpdatedEvent(SurvivorModel survivorModel, RoomModel roomModel, bool added)
        {
            SurvivorModel = survivorModel;
            RoomModel = roomModel;
            Added = added;
        }
    }
}