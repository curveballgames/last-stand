using Curveball;

namespace LastStand
{
    public class BaseModel : CBGGameObject
    {
        public string Name;
        public int MinSurvivorsNeeded;
        public int MinMaterialsNeeded;
        public RoomModel[] Rooms;
    }
}