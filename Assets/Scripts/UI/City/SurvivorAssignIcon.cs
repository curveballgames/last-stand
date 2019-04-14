using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Curveball;

namespace LastStand
{
    public class SurvivorAssignIcon : CBGGameObject, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private const float LERP_SPEED = 10000f;

        public RectTransform Draggable;
        public Image AssignmentColorImage;
        public RawImage SurvivorHeadshot;
        public SurvivorModel Model { get; set; }

        private bool dragging;
        private RenderTexture headTexture;

        private void LateUpdate()
        {
            if (dragging)
            {
                Draggable.position = Input.mousePosition;
                CityRaycaster.CastForBuildings();
            }
            else
            {
                Draggable.anchoredPosition = Vector2.Lerp(Draggable.anchoredPosition, Vector2.zero, Time.deltaTime * LERP_SPEED);
            }
        }

        public void UpdateView()
        {
            UpdateColor();
        }

        void UpdateColor()
        {
            if (Model.AssignedBuilding == null)
            {
                AssignmentColorImage.color = ColorDictionary.Singleton.ScavengerTeamAvailableColor;
            }
            else
            {
                AssignmentColorImage.color = ColorDictionary.Singleton.ScavengerTeamAssignedColor;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Curveball.EventSystem.Publish(new SurvivorIconHoveredEvent(this));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!dragging)
                Curveball.EventSystem.Publish(new SurvivorIconUnhoveredEvent(this));
        }

        public void OnDrag(PointerEventData eventData) { }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Input.GetMouseButton(0))
            {
                dragging = true;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!Input.GetMouseButton(0))
            {
                dragging = false;
                DropOnBuilding();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.dragging)
                return;

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Curveball.EventSystem.Publish(new ZoomToTargetEvent(Model));
            }
        }

        void DropOnBuilding()
        {
            CityBuildingModel hoveredBuilding = CityRaycaster.GetHoveredBuilding();

            if (hoveredBuilding != null && !(hoveredBuilding.IsExplored && !hoveredBuilding.OccupiableAfterSecuring))
            {
                Model.AssignToBuilding(hoveredBuilding);
                UpdateView();
            }
        }

        public void CreateHeadshot()
        {
            SurvivorHeadshot.texture = AvatarRenderCamera.RenderHeadshot(Model);
        }
    }
}