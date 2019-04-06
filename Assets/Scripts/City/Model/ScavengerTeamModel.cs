using Curveball;

namespace LastStand
{
    public class ScavengerTeamModel
    {
        public string Name;
        public RoomModel LinkedRoom;
        public CityBuildingModel AssignedBuilding { get; private set; }
        public CityBuildingModel.ResourceBundle ScavengedResources { get; private set; }

        private UnityEngine.Object subscriptionObject = new UnityEngine.Object();

        public ScavengerTeamModel()
        {
            ScavengedResources = new CityBuildingModel.ResourceBundle();
            EventSystem.Subscribe<RoomModelUpdatedEvent>(OnRoomModelUpdated, subscriptionObject);
            EventSystem.Subscribe<AdvanceDayPeriodEvent>(OnAdvanceDayPeriod, subscriptionObject);
        }

        ~ScavengerTeamModel()
        {
            EventSystem.Unsubscribe<RoomModelUpdatedEvent>(OnRoomModelUpdated, subscriptionObject);
            EventSystem.Unsubscribe<AdvanceDayPeriodEvent>(OnAdvanceDayPeriod, subscriptionObject);
        }

        void OnRoomModelUpdated(RoomModelUpdatedEvent e)
        {
            if (e.Room == LinkedRoom && !HasMembersAssigned() && AssignedBuilding != null)
            {
                AssignToBuilding(null);
            }
        }

        void OnAdvanceDayPeriod(AdvanceDayPeriodEvent e)
        {
            PlayerResources.Singleton.AddResources(ScavengedResources);
            ScavengedResources = new CityBuildingModel.ResourceBundle();

            if (AssignedBuilding != null && AssignedBuilding.IsExplored)
            {
                AssignToBuilding(null);
            }
        }

        public void AssignToBuilding(CityBuildingModel model)
        {
            AssignedBuilding = model;
            EventSystem.Publish(new ScavengerTeamAssignedEvent(this));
        }

        public bool HasMembersAssigned()
        {
            return LinkedRoom.AssignedSurvivors.Count > 0;
        }

        public void CarryOutAssignment()
        {
            if (AssignedBuilding == null || LinkedRoom.AssignedSurvivors.Count == 0)
                return;

            ScavengedResources = AssignedBuilding.Explore(this);
        }
    }
}