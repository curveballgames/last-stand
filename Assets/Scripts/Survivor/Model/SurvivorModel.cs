using System.Collections.Generic;

namespace LastStand
{
    [System.Serializable]
    public class SurvivorModel
    {
        public static readonly int[] SKILL_LEVEL_POINTS_REQUIRED = { 5, 10, 15, 20, 25 };
        private static int[] SKILL_LEVEL_POINT_CUMULATIVE_TOTALS;

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
            SKILL_LEVEL_POINT_CUMULATIVE_TOTALS = new int[SKILL_LEVEL_POINTS_REQUIRED.Length];

            int counter = 0;

            for (int i = 0; i < SKILL_LEVEL_POINTS_REQUIRED.Length; i++)
            {
                counter += SKILL_LEVEL_POINTS_REQUIRED[i];
                SKILL_LEVEL_POINT_CUMULATIVE_TOTALS[i] = counter;
            }
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

        public int GetLevel(int skillAmount)
        {
            for (int i = 0; i < SKILL_LEVEL_POINT_CUMULATIVE_TOTALS.Length; i++)
            {
                if (SKILL_LEVEL_POINT_CUMULATIVE_TOTALS[i] > skillAmount)
                    return i;
            }

            return SKILL_LEVEL_POINTS_REQUIRED.Length;
        }

        public int GetPointsTowardsNextLevel(int skillAmount)
        {
            for (int i = 0; i < SKILL_LEVEL_POINTS_REQUIRED.Length; i++)
            {
                if (SKILL_LEVEL_POINTS_REQUIRED[i] > skillAmount)
                    return skillAmount;

                skillAmount -= SKILL_LEVEL_POINTS_REQUIRED[i];
            }

            return 0;
        }

        public int GetTotalPointsNeededForNextLevel(int skillAmount)
        {
            for (int i = 0; i < SKILL_LEVEL_POINTS_REQUIRED.Length; i++)
            {
                if (SKILL_LEVEL_POINTS_REQUIRED[i] > skillAmount)
                    return SKILL_LEVEL_POINTS_REQUIRED[i];

                skillAmount -= SKILL_LEVEL_POINTS_REQUIRED[i];
            }

            return 0;
        }
    }
}