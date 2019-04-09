namespace LastStand
{
    public class SurvivorStatBar : ChunkedStatBar
    {
        private const string LABEL_FORMAT = "{0}/{1}";
        
        public SurvivorStatBarStar[] Stars;

        public void ConfigureForModel(SurvivorModel model, int statPoints, int previewValue)
        {
            int progress = model.GetPointsTowardsNextLevel(statPoints);
            int required = model.GetTotalPointsNeededForNextLevel(statPoints);
            int level = model.GetLevel(statPoints);

            MaxValue = required;
            Value = progress;
            PreviewValue = previewValue;

            UpdateBar();

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