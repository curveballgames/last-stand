using UnityEngine;
using Curveball;
using System.Collections.Generic;

namespace LastStand
{
    public class RoomAssignmentSlots : CBGUIComponent
    {
        private static readonly Vector3 WORLD_OFFSET = Vector3.up * 2f;

        private RoomView linkedRoom;
        private RoomAssignmentSlot[] assignmentSlots;

        private void LateUpdate()
        {
            RectTransform.position = Camera.main.WorldToScreenPoint(linkedRoom.transform.position + WORLD_OFFSET);
        }

        public void SetLinkedRoom(RoomView toLinkWith)
        {
            linkedRoom = toLinkWith;
            CreateAssignmentSlots();
        }

        void CreateAssignmentSlots()
        {
            assignmentSlots = new RoomAssignmentSlot[linkedRoom.LinkedModel.AssignmentSlots];

            for (int i = 0; i < linkedRoom.LinkedModel.AssignmentSlots; i++)
            {
                GameObject slot = Instantiate(PrefabDictionary.Singleton.RoomAssignmentSlotPrefab.gameObject, transform);
                assignmentSlots[i] = slot.GetComponent<RoomAssignmentSlot>();
            }
        }

        public void UpdateView(bool roomBuilt)
        {
            HashSet<SurvivorModel> survivorModels = linkedRoom.LinkedModel.AssignedSurvivors;

            int counter = 0;
            foreach (SurvivorModel survivor in survivorModels)
            {
                assignmentSlots[counter].ConfigureForSurvivor(survivor, roomBuilt);
                counter++;
            }

            for (; counter < assignmentSlots.Length; counter++)
            {
                assignmentSlots[counter].ConfigureForSurvivor(null, false);
            }
        }
    }
}