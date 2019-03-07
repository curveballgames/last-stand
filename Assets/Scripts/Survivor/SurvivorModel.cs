using System.Collections.Generic;

namespace LastStand
{
    [System.Serializable]
    public class SurvivorModel
    {
        public const int DEFAULT_HEALTH = 3;
        public const int MAX_SKILL_STAT = 100;

        public static List<SurvivorModel> AllModels { get; protected set; }

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

        public static void Initialise()
        {
            AllModels = new List<SurvivorModel>();
        }

        public SurvivorModel()
        {
            Name = string.Empty;
            Health = DEFAULT_HEALTH;
            Tiredness = 0;
            Hunger = 0;
            ShootingSkill = 0;
            FitnessSkill = 0;
            StrengthSkill = 0;

            AllModels.Add(this);
        }
    }
}