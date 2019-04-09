using UnityEngine;
using Curveball;
using UnityEngine.UI;
using TMPro;

namespace LastStand
{
    public class ScavengerReportPanel : CBGUIComponent
    {
        private static readonly string UNASSIGNED_KEY = "scavenger-ui:unassigned";
        private static readonly string NONE_KEY = "scavenger-ui:none";

        public TextMeshProUGUI TeamName;
        public TextMeshProUGUI AssignmentText;
        public RawImage TeamHeadshot;
        public Image IconBackground;
        [Space]
        public Transform AssignedBuildingParent;
        public TextMeshProUGUI FoodLabel;
        public TextMeshProUGUI MaterialsLabel;
        public ChunkedStatBar ExploredBar;

        public void ConfigureForTeam(ScavengerTeamModel model)
        {
            TeamName.text = model.Name;
            TeamHeadshot.texture = AvatarRenderCamera.RenderScavengerTeam(model);

            if (model.HasMembersAssigned())
            {
                if (model.AssignedBuilding == null)
                {
                    ConfigureForUnassignedTeam(model);
                }
                else
                {
                    ConfigureForAssignedTeam(model);
                }
            }
            else
            {
                ConfigureForEmptyTeam(model);
            }
        }

        void ConfigureForEmptyTeam(ScavengerTeamModel model)
        {
            IconBackground.color = ColorDictionary.Singleton.ScavengerTeamUnassignedColor;
            AssignmentText.text = LocalisationManager.GetValue(UNASSIGNED_KEY);
            AssignedBuildingParent.gameObject.SetActive(false);
        }

        void ConfigureForUnassignedTeam(ScavengerTeamModel model)
        {
            IconBackground.color = ColorDictionary.Singleton.ScavengerTeamAvailableColor;
            AssignmentText.text = LocalisationManager.GetValue(UNASSIGNED_KEY);
            AssignedBuildingParent.gameObject.SetActive(false);
        }

        void ConfigureForAssignedTeam(ScavengerTeamModel model)
        {
            IconBackground.color = ColorDictionary.Singleton.ScavengerTeamAssignedColor;
            AssignmentText.text = model.AssignedBuilding.Name;
            AssignedBuildingParent.gameObject.SetActive(true);

            if (model.ScavengedResources.Food > 0)
                FoodLabel.text = "+" + model.ScavengedResources.Food;
            else FoodLabel.text = LocalisationManager.GetValue(NONE_KEY);

            if (model.ScavengedResources.BuildingMaterials > 0)
                MaterialsLabel.text = "+" + model.ScavengedResources.BuildingMaterials;
            else MaterialsLabel.text = LocalisationManager.GetValue(NONE_KEY);

            ExploredBar.MaxValue = model.AssignedBuilding.RequiredExploreStages;
            ExploredBar.Value = model.AssignedBuilding.StagesExplored;
            ExploredBar.ForceUpdate();
        }
    }
}