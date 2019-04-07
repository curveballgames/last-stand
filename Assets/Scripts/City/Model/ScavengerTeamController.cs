using System.Collections.Generic;
using Curveball;

namespace LastStand
{
    public class ScavengerTeamController : CBGGameObject
    {
        private static readonly string TEAM_KEY = "scavenger-ui:team-name";
        private static readonly string TEAM_NAME_FORMAT = "{0} {1}";

        public static List<ScavengerTeamModel> ScavengerTeams { get; private set; }

        public static void CreateScavengerTeamsForCurrentBase()
        {
            ScavengerTeams = new List<ScavengerTeamModel>();
            int teamIndex = 1;

            foreach (RoomModel room in BaseModel.CurrentBase.Rooms)
            {
                if (room.RoomType == RoomType.Scavenger)
                {
                    ScavengerTeamModel model = new ScavengerTeamModel();
                    model.LinkedRoom = room;
                    model.Name = string.Format(TEAM_NAME_FORMAT, LocalisationManager.GetValue(TEAM_KEY), teamIndex);
                    ScavengerTeams.Add(model);
                    teamIndex++;
                }
            }
        }

        public static bool AreAnyTeamsAssigned(CityBuildingModel cityBuilding)
        {
            return GetTeamAssignedToCityBuilding(cityBuilding) != null;
        }

        public static ScavengerTeamModel GetTeamAssignedToCityBuilding(CityBuildingModel model)
        {
            foreach (ScavengerTeamModel scavengerTeam in ScavengerTeams)
            {
                if (scavengerTeam.AssignedBuilding == model)
                {
                    return scavengerTeam;
                }
            }

            return null;
        }
    }
}