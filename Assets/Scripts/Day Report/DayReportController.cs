using Curveball;

namespace LastStand
{
    public class DayReportController : CBGGameObject
    {
        private static readonly string ANYTHING_UNASSIGNED_KEY = "report-ui:unassigned-text";
        private static readonly string SURVIVORS_UNASSIGNED_KEY = "report-ui:survivors-unassigned-text";
        private static readonly string SCAVENGERS_UNASSIGNED_KEY = "report-ui:scavengers-unassigned-text";
        private static readonly string CANCEL_KEY = "general-ui:cancel";
        private static readonly string OK_KEY = "general-ui:confirm";

        private void Awake()
        {
            EventSystem.Subscribe<ConfirmAssignmentEvent>(OnConfirmAssignment, this);
        }

        void OnConfirmAssignment(ConfirmAssignmentEvent e)
        {
            bool scavengerTeamsUnassigned = false;
            bool survivorsUnassigned = false;

            foreach (ScavengerTeamModel model in ScavengerTeamController.ScavengerTeams)
            {
                if (model.HasMembersAssigned() && model.AssignedBuilding == null)
                {
                    scavengerTeamsUnassigned = true;
                    break;
                }
            }

            foreach (SurvivorModel model in SurvivorModel.AllModels)
            {
                if (model.AssignedRoom == null)
                {
                    survivorsUnassigned = true;
                    break;
                }
            }

            if (!survivorsUnassigned && !scavengerTeamsUnassigned)
            {
                OnModalOk();
                return;
            }

            string confirmText = LocalisationManager.GetValue(ANYTHING_UNASSIGNED_KEY);
            string survivorText = survivorsUnassigned ? LocalisationManager.GetValue(SURVIVORS_UNASSIGNED_KEY) : string.Empty;
            string scavengerText = scavengerTeamsUnassigned ? LocalisationManager.GetValue(SCAVENGERS_UNASSIGNED_KEY) : string.Empty;

            string cancelText = LocalisationManager.GetValue(CANCEL_KEY);
            string okText = LocalisationManager.GetValue(OK_KEY);

            confirmText = string.Format(confirmText, survivorText, scavengerText);
            EventSystem.Publish(new ShowModalEvent(confirmText, okText, OnModalOk, cancelText, null));
        }

        void OnModalOk()
        {
            foreach (SurvivorModel model in SurvivorModel.AllModels)
            {
                model.CarryOutAssignment();
            }

            foreach (ScavengerTeamModel model in ScavengerTeamController.ScavengerTeams)
            {
                model.CarryOutAssignment();
            }

            EventSystem.Publish(new AssignmentConfirmedEvent());
        }
    }
}