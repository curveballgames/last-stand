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
            { RoomType.Bedroom, "room-name:bedroom" },
            { RoomType.Scavenger, "room-name:scavenger" },
            { RoomType.Repairs, "room-name:repairs" }
        };

        public static Dictionary<RoomType, string> RoomDescriptionLocalisationKeys = new Dictionary<RoomType, string>
        {
            { RoomType.Empty, "room-description:empty" },
            { RoomType.Bedroom, "room-description:bedroom" },
            { RoomType.Scavenger, "room-description:scavenger" },
            { RoomType.Repairs, "room-description:repairs" }
        };

        public static Dictionary<RoomType, int> RoomBuildStages = new Dictionary<RoomType, int>
        {
            { RoomType.Empty, 0 },
            { RoomType.Bedroom, 3 }
        };

        public static Dictionary<RoomType, RoomStatModifiers> StatModifiers = new Dictionary<RoomType, RoomStatModifiers>
        {
            { RoomType.Empty, new RoomStatModifiers(-1, 0, 0, 0) },
            { RoomType.Bedroom, new RoomStatModifiers(-5, 0, 0, 0) },
            { RoomType.Scavenger, new RoomStatModifiers(2, 1, 1, 1) },
            { RoomType.Repairs, new RoomStatModifiers(2, 0, 1, 1) }
        };
    }
}