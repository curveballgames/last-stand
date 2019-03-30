using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LastStand
{
    public class SurvivorStatBar : Curveball.Strategy.Healthbar
    {
        private const string LABEL_FORMAT = "{0}/{1}";

        public Color UnfilledColor;

        public SurvivorStatBarStar[] Stars;
        public Transform BarFillParent;

        private List<Image> fillPieces = new List<Image>();

        public void ConfigureForModel(SurvivorModel model, int statPoints)
        {
            int progress = model.GetPointsTowardsNextLevel(statPoints);
            int required = model.GetTotalPointsNeededForNextLevel(statPoints);
            int level = model.GetLevel(statPoints);

            MaxValue = required;
            Value = progress;

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

        protected override void UpdateLeftToRightBar()
        {
            while (fillPieces.Count < MaxValue)
            {
                CreateFillPiece();
            }

            for (int i = 0; i < fillPieces.Count; i++)
            {
                if (i < Value)
                {
                    fillPieces[i].gameObject.SetActive(true);
                    fillPieces[i].color = ColorGradient.Evaluate(0f);
                }
                else if (i < MaxValue)
                {
                    fillPieces[i].gameObject.SetActive(true);
                    fillPieces[i].color = UnfilledColor;
                }
                else
                {
                    fillPieces[i].gameObject.SetActive(false);
                }
            }
        }

        void CreateFillPiece()
        {
            Image newFillPiece = Instantiate(PrefabDictionary.Singleton.ProgressBarFillPiece.gameObject, BarFillParent).GetComponent<Image>();
            fillPieces.Add(newFillPiece);
        }
    }
}