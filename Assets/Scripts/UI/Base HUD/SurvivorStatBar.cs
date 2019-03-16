using TMPro;

namespace LastStand
{
    public class SurvivorStatBar : Curveball.Strategy.Healthbar
    {
        private const string LABEL_FORMAT = "{0}/{1}";

        public TextMeshProUGUI Label;
        public SurvivorStatBarStar[] Stars;

        public void ConfigureForModel(SurvivorModel model, int statPoints)
        {
            int progress = model.GetPointsTowardsNextLevel(statPoints);
            int required = model.GetTotalPointsNeededForNextLevel(statPoints);
            int level = model.GetLevel(statPoints);

            MaxValue = required;
            Value = progress;

            UpdateBar();

            Label.text = string.Format(LABEL_FORMAT, progress, required);

            for (int i = 0; i < Stars.Length; i++)
            {
                if (i < level)
                {
                    Stars[i].SetFilled();
                }
                else
                {
                    Stars[i].SetUnfilled();
                }
            }
        }
    }
}