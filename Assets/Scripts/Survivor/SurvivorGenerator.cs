using UnityEngine;

namespace LastStand
{
    public class SurvivorGenerator
    {
        private const int INITIAL_SURVIVORS = 3;
        private const int GENERIC_SURVIVOR_STAT_VARIANCE = 20;

        public static void GenerateInitialSurvivors()
        {
            for (int i = 0; i < INITIAL_SURVIVORS - 1; i++)
            {
                GenerateSurvivorModel();
            }

            GenerateHeroicSurvivorModel();
        }

        public static void GenerateSurvivorModel()
        {
            SurvivorModel model = new SurvivorModel();

            model.FitnessSkill = Random.Range(0, GENERIC_SURVIVOR_STAT_VARIANCE + 1);
            model.StrengthSkill = Random.Range(0, GENERIC_SURVIVOR_STAT_VARIANCE + 1);
            model.ShootingSkill = Random.Range(0, GENERIC_SURVIVOR_STAT_VARIANCE + 1);
            model.Name = SurvivorNameGenerator.GenerateName(Random.value > 0.5f);
        }

        private const int HEROIC_SURVIVOR_STAT_MIN = 15;
        private const int HEROIC_SURVIVOR_STAT_MAX = 50;

        public static void GenerateHeroicSurvivorModel()
        {
            SurvivorModel model = new SurvivorModel();

            model.FitnessSkill = Random.Range(HEROIC_SURVIVOR_STAT_MIN, GENERIC_SURVIVOR_STAT_VARIANCE + 1);
            model.StrengthSkill = Random.Range(HEROIC_SURVIVOR_STAT_MIN, HEROIC_SURVIVOR_STAT_MAX + 1);
            model.ShootingSkill = Random.Range(HEROIC_SURVIVOR_STAT_MIN, HEROIC_SURVIVOR_STAT_MAX + 1);
            model.Name = SurvivorNameGenerator.GenerateName(Random.value > 0.5f);
        }
    }
}