using Curveball;

namespace LastStand
{
    public class OutdoorEntityModel : CBGGameObject
    {
        public OutdoorEntityType EntityType;
        public int MaxHealth;
        public int Health;

        public void CopyFrom(OutdoorEntityModel other)
        {
            Health = other.Health;
        }
    }
}