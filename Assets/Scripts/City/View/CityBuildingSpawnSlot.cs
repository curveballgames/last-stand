using Curveball;

namespace LastStand
{
    public class CityBuildingSpawnSlot : CBGGameObject
    {
        public CityBuildingType[] AcceptedTypes;
        public int Width;
        public int Height;

        public override string ToString()
        {
            string val = string.Format("Width: {0}\tHeight: {1}\nAccepted types: ", Width, Height);

            foreach (CityBuildingType buildingType in AcceptedTypes)
            {
                val += buildingType.ToString() + "\t";
            }

            return val;
        }
    }
}