using UnityEngine;
using Curveball;
using UnityEngine.UI;

namespace LastStand
{
    public class RoomBuildProgressBar : CBGUIComponent
    {
        private static readonly Vector3 OFFSET = new Vector3(0f, -40f, 0f);

        public Color FilledColor;
        public Color UnfilledColor;
        public Color PreviewColor;

        public Image[] BarFill;
        public RectTransform OffsetParent;

        private void LateUpdate()
        {
            RectTransform.position = OffsetParent.position + OFFSET;
        }

        public void UpdateView(RoomModel model)
        {
            int buildStagesRequired = RoomTypeDictionary.RoomBuildStages[model.RoomType];
            int stagesBuilt = model.BuildProgress;
            int stagesCompletedThisCycle = model.AssignedSurvivors.Count;

            for (int i = 0; i < BarFill.Length; i++)
            {
                BarFill[i].gameObject.SetActive(i < buildStagesRequired);

                if (i < stagesBuilt)
                {
                    BarFill[i].color = FilledColor;
                }
                else if (i < stagesBuilt + stagesCompletedThisCycle)
                {
                    BarFill[i].color = PreviewColor;
                }
                else
                {
                    BarFill[i].color = UnfilledColor;
                }
            }
        }
    }
}