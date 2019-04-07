using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LastStand
{
    public class ChunkedStatBar : Curveball.Strategy.Healthbar
    {
        public Color UnfilledColor;
        public Color PreviewColor;

        public Transform BarFillParent;
        public int PreviewValue { get; set; }

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
                fillPieces[i].gameObject.SetActive(i < MaxValue);

                if (i < Value)
                {
                    fillPieces[i].color = ColorGradient.Evaluate(0f);
                }
                else if (i < MaxValue && i < Value + PreviewValue)
                {
                    fillPieces[i].color = PreviewColor;
                }
                else if (i < MaxValue)
                {
                    fillPieces[i].color = UnfilledColor;
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