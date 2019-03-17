using System.Collections.Generic;

namespace LastStand
{
    public class RoomTypeDictionary
    {
        public static Dictionary<RoomType, int> Costs = new Dictionary<RoomType, int>
        {
            { RoomType.Bedroom, 5 }
        };

        public static Dictionary<RoomType, string> RoomNameLocalisationKeys = new Dictionary<RoomType, string>
        {
            { RoomType.Empty, "room-name:empty" },
            { RoomType.Bedroom, "room-name:bedroom" }
        };

        public static Dictionary<RoomType, string> RoomDescriptionLocalisationKeys = new Dictionary<RoomType, string>
        {
            { RoomType.Empty, "room-description:empty" },
            { RoomType.Bedroom, "room-description:bedroom" }
        };
    }
}