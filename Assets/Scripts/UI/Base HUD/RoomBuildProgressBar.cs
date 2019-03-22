using UnityEngine;
using Curveball;
using UnityEngine.UI;

namespace LastStand
{
    public class RoomBuildProgressBar : CBGUIComponent
    {
        private static readonly string CANCEL_BODY_LOCALISATION_KEY = "build-ui:cancel-construction-content";
        private static readonly string CONFIRM_LOCALISATION_KEY = "build-ui:confirm-cancel";
        private static readonly string CANCEL_LOCALISATION_KEY = "build-ui:cancel-cancel";

        private static readonly Vector3 OFFSET = new Vector3(0f, -40f, 0f);

        public Color FilledColor;
        public Color UnfilledColor;
        public Color PreviewColor;

        public Image[] BarFill;
        public RectTransform OffsetParent;
        public Button CancelButton;

        private RoomModel linkedModel;

        private void Awake()
        {
            CancelButton.onClick.AddListener(OnCancelConstruction);
        }

        private void LateUpdate()
        {
            RectTransform.position = OffsetParent.position + OFFSET;
        }

        public void UpdateView(RoomModel model)
        {
            linkedModel = model;

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

        void OnCancelConstruction()
        {
            string body = LocalisationManager.GetValue(CANCEL_BODY_LOCALISATION_KEY);
            string okButton = LocalisationManager.GetValue(CONFIRM_LOCALISATION_KEY);
            string cancelButton = LocalisationManager.GetValue(CANCEL_LOCALISATION_KEY);

            EventSystem.Publish(new ShowModalEvent(body, okButton, () =>
            {
                if (linkedModel != null)
                {
                    linkedModel.CancelConstruction();
                }
            }, cancelButton, null));
        }
    }
}