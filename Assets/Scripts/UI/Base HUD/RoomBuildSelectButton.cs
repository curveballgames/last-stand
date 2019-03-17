using Curveball;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LastStand
{
    public class RoomBuildSelectButton : CBGUIComponent, IPointerEnterHandler, IPointerExitHandler
    {
        public Button Button;
        public RoomType TypeToBuild;

        public RoomModel LinkedModel { get; set; }

        private void Awake()
        {
            Button.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            Curveball.EventSystem.Publish(new StartRoomConstructionEvent(TypeToBuild, LinkedModel));
        }

        public void UpdateState()
        {
            Button.interactable = RoomTypeDictionary.Costs[TypeToBuild] <= PlayerResources.Singleton.BuildingMaterials;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Curveball.EventSystem.Publish(new RoomBuildButtonHoveredEvent(this));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Curveball.EventSystem.Publish(new RoomBuildButtonUnhoveredEvent(this));
        }
    }
}