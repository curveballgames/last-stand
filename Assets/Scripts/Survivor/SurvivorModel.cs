using System.Collections.Generic;

namespace LastStand
{
    [System.Serializable]
    public class SurvivorModel
    {
        public const int DEFAULT_HEALTH = 3;
        public const int MAX_SKILL_STAT = 100;

        public static List<SurvivorModel> AllModels { get; protected set; }
        private static int currentId;

        static SurvivorModel()
        {
            Initialise();
        }

        public string Name;
        public int Health;
        public int Tiredness;
        public int Hunger;
        public int FitnessSkill;
        public int StrengthSkill;
        public int ShootingSkill;
        public int Id;

        public static void Initialise()
        {
            AllModels = new List<SurvivorModel>();
            currentId = 0;
        }

        public SurvivorModel()
        {
            Name = string.Empty;
            Health = DEFAULT_HEALTH;
            Tiredness = 0;
            Hunger = 0;
            ShootingSkill = 0;
            FitnessSkill = 0;
            StrengthSkill = 0;Id = currentId++;

            AllModels.Add(this);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            SurvivorModel otherModel = obj as SurvivorModel;

            if (otherModel == null)
                return false;

            return otherModel.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override string ToString()
        {
            return Name + Id;
        }
    }
}