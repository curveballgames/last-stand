using Curveball;

namespace LastStand
{
    public struct SurvivorAssignmentUpdatedEvent : IEvent
    {
        public SurvivorModel SurvivorModel;
        public CityBuildingModel CityBuilding;
        public bool Added;

        public SurvivorAssignmentUpdatedEvent(SurvivorModel survivorModel, CityBuildingModel cityBuilding, bool added)
        {
            SurvivorModel = survivorModel;
            CityBuilding = cityBuilding;
            Added = added;
        }
    }
}