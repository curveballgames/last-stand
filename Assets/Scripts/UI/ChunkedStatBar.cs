using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LastStand
{
    public class ChunkedStatBar : Curveball.Strategy.Healthbar
    {
        public Color UnfilledColor;
        public Transform BarFillParent;

        private List<Image> fillPieces = new List<Image>();

        public void ForceUpdate()
        {
            UpdateBar();
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