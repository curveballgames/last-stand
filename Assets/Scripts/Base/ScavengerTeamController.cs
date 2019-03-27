using System.Collections.Generic;
using Curveball;

namespace LastStand
{
    public class ScavengerTeamController : CBGGameObject
    {
        public static List<ScavengerTeamModel> ScavengerTeams { get; private set; }

        public static void CreateScavengerTeamsForCurrentBase()
        {
            ScavengerTeams = new List<ScavengerTeamModel>();

            foreach (RoomModel room in BaseModel.CurrentBase.Rooms)
            {
                if (room.RoomType == RoomType.Scavenger)
                {
                    ScavengerTeamModel model = new ScavengerTeamModel();
                    model.LinkedRoom = room;
                    ScavengerTeams.Add(model);
                }
            }
        }

        public static bool AreAnyTeamsAssigned(CityBuildingModel cityBuilding)
        {
            foreach (ScavengerTeamModel scavengerTeam in ScavengerTeams)
            {
                if (scavengerTeam.AssignedBuilding == cityBuilding)
                {
                    return true;
                }
            }

            return false;
        }
    }
}