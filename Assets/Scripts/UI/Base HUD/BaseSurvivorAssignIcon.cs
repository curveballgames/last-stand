using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Curveball;

namespace LastStand
{
    public class BaseSurvivorAssignIcon : CBGGameObject, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private const float LERP_SPEED = 12f;

        public RectTransform Draggable;
        public Image AssignmentColorImage;
        public SurvivorModel Model { get; set; }

        private bool dragging;
        private RoomHoverEvent roomHoverEvent = new RoomHoverEvent();

        private void LateUpdate()
        {
            if (dragging)
            {
                Draggable.position = Input.mousePosition;
                roomHoverEvent.Room = BaseRaycaster.CastForRooms();
                Curveball.EventSystem.Publish(roomHoverEvent);
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

                if (roomHoverEvent.Room != null)
                {
                    DropOnRoom();
                    roomHoverEvent.Room = null;
                    Curveball.EventSystem.Publish(roomHoverEvent);
                }
            }
        }

        void DropOnRoom()
        {
            if (roomHoverEvent.Room != null && roomHoverEvent.Room.RoomType != RoomType.Empty && !roomHoverEvent.Room.IsFull())
            {
                Model.AssignRoom(roomHoverEvent.Room);
                UpdateView();
            }
        }
    }
}