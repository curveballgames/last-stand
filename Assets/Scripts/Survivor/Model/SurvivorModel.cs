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

        public static readonly int[] SKILL_LEVEL_POINTS_REQUIRED = { 5, 10, 15, 20, 25 };
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

        public RoomModel AssignedRoom { get; private set; }
        public int? AssignedRoomGUID;

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

        public void AssignRoom(RoomModel model)
        {
            AssignedRoom = model;

            if (model == null)
                AssignedRoomGUID = null;
            else AssignedRoomGUID = model.GetInstanceID();

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
            if (AssignedRoom != null && !AssignedRoom.IsBuilt)
            {
                CarryOutConstruction();
                return;
            }

            RoomType currentAssignmentType = RoomType.Empty;

            // need to check if the scavenger team is assigned to a building, otherwise survivor is idle
            if (AssignedRoom != null && AssignedRoom.RoomType == RoomType.Scavenger)
            {
                foreach (ScavengerTeamModel model in ScavengerTeamController.ScavengerTeams)
                {
                    if (model.LinkedRoom == AssignedRoom && model.AssignedBuilding != null)
                    {
                        currentAssignmentType = RoomType.Scavenger;
                    }
                }
            }
            else if (AssignedRoom != null)
            {
                currentAssignmentType = AssignedRoom.RoomType;
            }

            RoomStatModifiers statMods = RoomTypeDictionary.StatModifiers[currentAssignmentType];

            // TODO: clamp if at max values
            TirednessChange = statMods.TirednessChange;
            FitnessChange = statMods.FitnessChange;
            ShootingChange = statMods.ShootingChange;
            StrengthChange = statMods.StrengthChange;

            Tiredness = Mathf.Clamp(Tiredness + Tiredness, 0, MAX_TIREDNESS);
            FitnessSkill = Mathf.Clamp(FitnessSkill + FitnessChange, 0, MAX_SKILL_LEVEL);
            StrengthSkill = Mathf.Clamp(StrengthSkill + StrengthChange, 0, MAX_SKILL_LEVEL);
            ShootingSkill = Mathf.Clamp(ShootingSkill + ShootingChange, 0, MAX_SKILL_LEVEL);
        }

        void CarryOutConstruction()
        {
            AssignedRoom.ContributeTowardsConstruction(this);

            TirednessChange = CONSTRUCTION_TIREDNESS_CHANGE;
            StrengthChange = CONSTRUCTION_STRENGTH_CHANGE;
            Tiredness = Mathf.Clamp(Tiredness + Tiredness, 0, MAX_TIREDNESS);
            StrengthSkill = Mathf.Clamp(StrengthSkill + StrengthChange, 0, MAX_SKILL_LEVEL);
        }
    }
}