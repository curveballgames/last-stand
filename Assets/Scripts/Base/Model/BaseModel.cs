using Curveball;
using UnityEngine;

namespace LastStand
{
    public class BaseModel : CBGGameObject
    {
        public static BaseModel CurrentBase;

        public string Name;
        public int MinSurvivorsNeeded;
        public int MinMaterialsNeeded;
        public RoomModel[] Rooms;
        public OutdoorEntityModel[] OutdoorEntities;
        public Vector3 Center;
        public Vector2 Bounds;

        public void CopyFrom(BaseModel other)
        {
            for (int i = 0; i < Rooms.Length; i++)
            {
                Rooms[i].CopyFrom(other.Rooms[i]);
            }

            for (int i = 0; i < OutdoorEntities.Length; i++)
            {
                OutdoorEntities[i].CopyFrom(other.OutdoorEntities[i]);
            }
        }
    }
}