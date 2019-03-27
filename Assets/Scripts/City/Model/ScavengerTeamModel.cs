using Curveball;

namespace LastStand
{
    public class ScavengerTeamModel
    {
        public RoomModel LinkedRoom;
        public CityBuildingModel AssignedBuilding { get; private set; }

        private UnityEngine.Object subscriptionObject = new UnityEngine.Object();

        public ScavengerTeamModel()
        {
            EventSystem.Subscribe<RoomModelUpdatedEvent>(OnRoomModelUpdated, subscriptionObject);
        }

        ~ScavengerTeamModel()
        {
            EventSystem.Unsubscribe<RoomModelUpdatedEvent>(OnRoomModelUpdated, subscriptionObject);
        }

        void OnRoomModelUpdated(RoomModelUpdatedEvent e)
        {
            if (e.Room == LinkedRoom && !HasMembersAssigned() && AssignedBuilding != null)
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
    }
}