using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curveball;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LastStand
{
    public class ScavengerTeamAssignmentIcon : CBGGameObject, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public Color AssignedColor;
        public Color AvailableColor;
        public Color UnassignedColor;

        public RectTransform Draggable;
        public Image AssignmentColorImage;
        public RawImage Headshots;
        public ScavengerTeamModel Model { get; set; }

        private bool dragging;
        private ScavengerTeamPointer pointer;

        private void Awake()
        {
            pointer = Instantiate(PrefabDictionary.Singleton.ScavengerTeamAssignmentPointerPrefab.gameObject, UIManager.Singleton.ScavengerTeamPointers).GetComponent<ScavengerTeamPointer>();
            pointer.SetActive(false);
        }

        private void LateUpdate()
        {
            if (dragging)
            {
                Draggable.position = Input.mousePosition;
                CityRaycaster.CastForBuildings();
            }
            else
            {
                Draggable.anchoredPosition = Vector2.zero;
            }
        }

        public void UpdateView()
        {
            UpdateColor();
            RecreateHeadshots();
            UpdatePointer();
        }

        void UpdateColor()
        {
            if (!Model.HasMembersAssigned())
            {
                AssignmentColorImage.color = UnassignedColor;
            }
            else
            {
                if (Model.AssignedBuilding == null)
                {
                    AssignmentColorImage.color = AvailableColor;
                }
                else
                {
                    AssignmentColorImage.color = AssignedColor;
                }
            }
        }

        void UpdatePointer()
        {
            if (Model.HasMembersAssigned() && Model.AssignedBuilding != null)
            {
                pointer.BuildingToTrack = Model.AssignedBuilding;
                pointer.SetHeadshotTexture(Headshots.texture as RenderTexture);
                pointer.SetActive(true);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Curveball.EventSystem.Publish(new ScavengerTeamIconHoveredEvent(this));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!dragging)
                Curveball.EventSystem.Publish(new ScavengerTeamIconUnhoveredEvent(this));
        }

        public void OnDrag(PointerEventData eventData) { }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Input.GetMouseButton(0) && Model.HasMembersAssigned())
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

        void DropOnBuilding()
        {
            CityBuildingModel hoveredBuilding = CityRaycaster.GetHoveredBuilding();

            if (hoveredBuilding != null && !ScavengerTeamController.AreAnyTeamsAssigned(hoveredBuilding))
            {
                Model.AssignToBuilding(hoveredBuilding);
                UpdateView();
            }
        }

        void RecreateHeadshots()
        {
            if (Model.HasMembersAssigned())
            {
                Headshots.texture = AvatarRenderCamera.RenderScavengerTeam(Model);
            }
            else
            {
                if (Headshots.texture != null)
                {
                    (Headshots.texture as RenderTexture).Release();
                    Headshots.texture = null;
                }
            }
        }
    }
}