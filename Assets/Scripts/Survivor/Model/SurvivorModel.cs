using Curveball;
using System.Collections.Generic;
using UnityEngine;

namespace LastStand
{
    [System.Serializable]
    public class SurvivorModel
    {
        public const int MAX_TIREDNESS = 5;
        public const int MAX_HUNGER = 5;

        public const int DEFAULT_HEALTH = 3;

        private const int CONSTRUCTION_TIREDNESS_CHANGE = 2;
        private const int CONSTRUCTION_STRENGTH_CHANGE = 1;

        public static readonly int[] SKILL_LEVEL_POINTS_REQUIRED = { 4, 5, 6, 7, 8 };
        public static List<SurvivorModel> AllModels { get; protected set; }
        public static int MAX_SKILL_LEVEL { get => SKILL_LEVEL_POINTS_REQUIRED[SKILL_LEVEL_POINTS_REQUIRED.Length - 1]; }

        private static int[] SKILL_LEVEL_POINT_CUMULATIVE_TOTALS;

        static SurvivorModel()
        {
            Initialise();
        }

        public string Name;
        public int Health;
        public int HealthChange { get; private set; }
        public int Tiredness;
        public int TirednessChange { get; private set; }
        public int Hunger;
        public int HungerChange { get; private set; }
        public int FitnessSkill;
        public int FitnessChange { get; private set; }
        public int StrengthSkill;
        public int StrengthChange { get; private set; }
        public int ShootingSkill;
        public int ShootingChange { get; private set; }

        public bool IsMale;
        public int SkinTone;
        public int HairStyle;
        public int HairColour;

        public CityBuildingModel AssignedBuilding { get; private set; }
        public int? AssignedBuildingGUID;

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
            IsMale = true;
            Health = DEFAULT_HEALTH;
            Tiredness = 0;
            Hunger = 0;
            ShootingSkill = 0;
            FitnessSkill = 0;
            StrengthSkill = 0;
            SkinTone = 0;
            HairStyle = 0;
            HairColour = 0;

            AllModels.Add(this);
        }

        public void RandomiseValues()
        {
            IsMale = Random.value > 0.5f;
            SkinTone = Random.Range(0, SurvivorAvatarGenerator.NumSkinTones);
            HairStyle = Random.Range(0, IsMale ? SurvivorAvatarGenerator.NumMaleHairstyles : SurvivorAvatarGenerator.NumFemaleHairstyles);
            HairColour = Random.Range(0, SurvivorAvatarGenerator.NumHairColours);
        }

        public void AssignToBuilding(CityBuildingModel model)
        {
            AssignedBuilding = model;

            if (model == null)
                AssignedBuildingGUID = null;
            else AssignedBuildingGUID = model.GetInstanceID();

            EventSystem.Publish(new SurvivorAssignmentUpdatedEvent(this, model, model != null));
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

        public void CarryOutAssignment()
        {
            // TODO

            //if (AssignedBuilding != null && !AssignedBuilding.IsBuilt)
            //{
            //    CarryOutConstruction();
            //    return;
            //}

            //RoomType currentAssignmentType = RoomType.Empty;
            
            //if (AssignedBuilding != null && AssignedBuilding.RoomType == RoomType.Scavenger)
            //{
            //    foreach (ScavengerTeamModel model in ScavengerTeamController.ScavengerTeams)
            //    {
            //        if (model.LinkedRoom == AssignedBuilding && model.AssignedBuilding != null)
            //        {
            //            currentAssignmentType = RoomType.Scavenger;
            //        }
            //    }
            //}
            //else if (AssignedBuilding != null)
            //{
            //    currentAssignmentType = AssignedBuilding.RoomType;
            //}

            //RoomStatModifiers statMods = RoomTypeDictionary.StatModifiers[currentAssignmentType];
            
            //TirednessChange = statMods.TirednessChange;
            //FitnessChange = statMods.FitnessChange;
            //ShootingChange = statMods.ShootingChange;
            //StrengthChange = statMods.StrengthChange;

            //Tiredness = Mathf.Clamp(Tiredness + Tiredness, 0, MAX_TIREDNESS);
            //FitnessSkill = Mathf.Clamp(FitnessSkill + FitnessChange, 0, MAX_SKILL_LEVEL);
            //StrengthSkill = Mathf.Clamp(StrengthSkill + StrengthChange, 0, MAX_SKILL_LEVEL);
            //ShootingSkill = Mathf.Clamp(ShootingSkill + ShootingChange, 0, MAX_SKILL_LEVEL);
        }
    }
}