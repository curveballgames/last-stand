using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Curveball;

namespace LastStand
{
    public class BaseSurvivorAssignIcon : CBGGameObject, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
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
                BaseRaycaster.CastForRooms();
            }
            else
            {
                Draggable.anchoredPosition = Vector2.Lerp(Draggable.anchoredPosition, Vector2.zero, Time.deltaTime * LERP_SPEED);
            }
        }

        public void UpdateView()
        {
            AssignmentColorImage.color = SurvivorAvatarGenerator.GetColorForRoom(Model.AssignedRoom);
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

        public void OnPointerClick(PointerEventData eventData)
        {
            Curveball.EventSystem.Publish(new SurvivorIconClickedEvent(this));
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
                DropOnRoom();
            }
        }

        void DropOnRoom()
        {
            RoomModel hoveredRoom = BaseRaycaster.GetHoveredRoom();

            if (hoveredRoom != null && hoveredRoom.RoomType != RoomType.Empty && !hoveredRoom.IsFull())
            {
                Model.AssignRoom(hoveredRoom);
                UpdateView();
            }
        }

        public void CreateHeadshot()
        {
            SurvivorHeadshot.texture = AvatarRenderCamera.RenderHeadshot(Model);
        }
    }
}