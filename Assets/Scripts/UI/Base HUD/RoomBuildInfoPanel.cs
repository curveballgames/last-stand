using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curveball;
using TMPro;

namespace LastStand
{
    public class RoomBuildInfoPanel : CBGUIComponent
    {
        public TextMeshProUGUI RoomName;
        public TextMeshProUGUI RoomDescription;
        public TextMeshProUGUI RoomBuildCost;

        public void ConfigureForType(RoomType typeToBuild)
        {
            RoomName.text = LocalisationManager.GetValue(RoomTypeDictionary.RoomNameLocalisationKeys[typeToBuild]);
            RoomDescription.text = LocalisationManager.GetValue(RoomTypeDictionary.RoomDescriptionLocalisationKeys[typeToBuild]);
            RoomBuildCost.text = RoomTypeDictionary.Costs[typeToBuild].ToString();
        }
    }
}