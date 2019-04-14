namespace LastStand
{
    [System.Serializable]
    public struct CityBuildingStatModifiers
    {
        public int ShootingChange;
        public int FitnessChange;
        public int StrengthChange;
        public int TirednessChange;

        public CityBuildingStatModifiers(int shootingChange, int fitnessChange, int strengthChange, int tirednessChange)
        {
            ShootingChange = shootingChange;
            FitnessChange = fitnessChange;
            StrengthChange = strengthChange;
            TirednessChange = tirednessChange;
        }
    }
}