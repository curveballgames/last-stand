using UnityEngine;
using Curveball;

namespace LastStand
{
    public class ConstructionMenu : CBGUIComponent
    {
        public GameObject BuildMenu;
        public RoomBuildInfoPanel InfoPanel;
        public CanvasGroupFader Fader;

        public RoomBuildSelectButton[] SelectButtons;

        private RoomBuildSelectButton hoveredButton;

        private void Awake()
        {
            EventSystem.Subscribe<BuildButtonPressedEvent>(OnBuildButtonPressed, this);
            EventSystem.Subscribe<StartRoomConstructionEvent>(OnStartConstruction, this);

            EventSystem.Subscribe<RoomBuildButtonHoveredEvent>(OnRoomBuildButtonHovered, this);
            EventSystem.Subscribe<RoomBuildButtonUnhoveredEvent>(OnRoomBuildButtonUnhovered, this);

            Close();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel") && BuildMenu.activeSelf)
            {
                Close();
            }
        }

        void OnBuildButtonPressed(BuildButtonPressedEvent e)
        {
            foreach (RoomBuildSelectButton sb in SelectButtons)
            {
                sb.LinkedModel = e.LinkedModel;
                sb.UpdateState();
            }

            Open();
        }

        void OnStartConstruction(StartRoomConstructionEvent e)
        {
            foreach (RoomBuildSelectButton sb in SelectButtons)
            {
                sb.LinkedModel = null;
            }

            Close();
        }

        void OnRoomBuildButtonHovered(RoomBuildButtonHoveredEvent e)
        {
            hoveredButton = e.SelectButton;
            ShowInfoPanel(e.SelectButton.TypeToBuild);
        }

        void OnRoomBuildButtonUnhovered(RoomBuildButtonUnhoveredEvent e)
        {
            if (hoveredButton == e.SelectButton)
            {
                HideInfoPanel();
                hoveredButton = null;
            }
        }

        void Open()
        {
            Fader.FadeIn();
            HideInfoPanel();
            hoveredButton = null;
        }

        void Close()
        {
            Fader.FadeOut();
            HideInfoPanel();
            hoveredButton = null;
        }

        void ShowInfoPanel(RoomType typeToBuild)
        {
            InfoPanel.ConfigureForType(typeToBuild);
            InfoPanel.SetActive(true);
        }

        void HideInfoPanel()
        {
            InfoPanel.SetActive(false);
        }
    }
}