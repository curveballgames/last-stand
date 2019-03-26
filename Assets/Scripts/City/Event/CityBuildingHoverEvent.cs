using Curveball;

namespace LastStand
{
    public class CityBuildingHoverEvent : IEvent
    {
        public CityBuildingModel Building;

        public CityBuildingHoverEvent(CityBuildingModel building)
        {
            Building = building;
        }
    }
}