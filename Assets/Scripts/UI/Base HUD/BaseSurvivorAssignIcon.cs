using System.Collections;
using System.Collections.Generic;
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

        private void LateUpdate()
        {
            if (dragging)
            {
                Draggable.position = Input.mousePosition;
            }
            else
            {
                Draggable.anchoredPosition = Vector2.Lerp(Draggable.anchoredPosition, Vector2.zero, Time.deltaTime * LERP_SPEED);
            }
        }

        public void UpdateView()
        {

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Curveball.EventSystem.Publish(new SurvivorIconHoveredEvent(this));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Curveball.EventSystem.Publish(new SurvivorIconUnhoveredEvent(this));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Curveball.EventSystem.Publish(new SurvivorIconClickedEvent(this));
        }

        public void OnDrag(PointerEventData eventData)
        { }

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
            }
        }
    }
}