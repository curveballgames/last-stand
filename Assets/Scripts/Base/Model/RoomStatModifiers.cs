namespace LastStand
{
    [System.Serializable]
    public struct RoomStatModifiers
    {
        public int TirednessChange;
        public int ShootingChange;
        public int FitnessChange;
        public int StrengthChange;

        public RoomStatModifiers(int tirednessChange, int shootingChange, int fitnessChange, int strengthChange)
        {
            TirednessChange = tirednessChange;
            ShootingChange = shootingChange;
            FitnessChange = fitnessChange;
            StrengthChange = strengthChange;
        }
    }
}