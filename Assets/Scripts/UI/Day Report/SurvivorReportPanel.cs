using UnityEngine;
using Curveball;
using TMPro;
using UnityEngine.UI;

namespace LastStand
{
    public class SurvivorReportPanel : CBGUIComponent
    {
        private static readonly string VERY_GOOD_STAT_TEXT = "++";
        private static readonly string GOOD_STAT_TEXT = "+";
        private static readonly string NEUTRAL_STAT_TEXT = "~";
        private static readonly string BAD_STAT_TEXT = "-";
        private static readonly string VERY_BAD_STAT_TEXT = "--";

        public Color GoodStatColor;
        public Color BadStatColor;
        public Color NeutralStatColor;

        public TextMeshProUGUI SurvivorName;
        public RawImage SurvivorHeadshot;
        public Image IconBackground;

        public TextMeshProUGUI ShootingStatChange;
        public TextMeshProUGUI FitnessStatChange;
        public TextMeshProUGUI StrengthStatChange;
        public TextMeshProUGUI TirednessStatChange;
        public TextMeshProUGUI HealthStatChange;
        public TextMeshProUGUI HungerStatChange;

        public void ConfigureForSurvivor(SurvivorModel s)
        {
            SurvivorName.text = s.Name;
            SurvivorHeadshot.texture = AvatarRenderCamera.RenderHeadshot(s);
            //IconBackground.color = SurvivorAvatarGenerator.GetColorForRoom(s.AssignedBuilding); // TODO

            ConfigureStat(ref ShootingStatChange, s.ShootingChange, false);
            ConfigureStat(ref FitnessStatChange, s.FitnessChange, false);
            ConfigureStat(ref StrengthStatChange, s.StrengthChange, false);
            ConfigureStat(ref TirednessStatChange, s.TirednessChange, true);
            ConfigureStat(ref HealthStatChange, s.HealthChange, true);
            ConfigureStat(ref HungerStatChange, s.HungerChange, true);
        }

        void ConfigureStat(ref TextMeshProUGUI textInstance, int statChange, bool flipPosNeg)
        {
            if (statChange == 0)
            {
                textInstance.color = NeutralStatColor;
                textInstance.text = NEUTRAL_STAT_TEXT;
            }
            else if (statChange > 0)
            {
                textInstance.color = flipPosNeg ? BadStatColor : GoodStatColor;
                textInstance.text = statChange > 1 ? VERY_GOOD_STAT_TEXT : GOOD_STAT_TEXT;
            }
            else if (statChange < 0)
            {
                textInstance.color = flipPosNeg ? GoodStatColor : BadStatColor;
                textInstance.text = statChange < -1 ? VERY_BAD_STAT_TEXT : BAD_STAT_TEXT;
            }
        }
    }
}