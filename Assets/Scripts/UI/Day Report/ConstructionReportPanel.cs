using Curveball;
using TMPro;

namespace LastStand
{
    public class ConstructionReportPanel : CBGUIComponent
    {
        public TextMeshProUGUI Title;
        public ChunkedStatBar ProgressBar;

        public void ConfigureForRoom(RoomModel model)
        {
            Title.text = LocalisationManager.GetValue(RoomTypeDictionary.RoomNameLocalisationKeys[model.RoomType]);

            ProgressBar.Value = model.BuildProgress;
            ProgressBar.MaxValue = RoomTypeDictionary.RoomBuildStages[model.RoomType];
            ProgressBar.PreviewValue = 0;
            ProgressBar.ForceUpdate();
        }
    }
}