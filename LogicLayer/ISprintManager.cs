using DataObjects;
using System.Collections.Generic;

namespace LogicLayer {
    public interface ISprintManager {
        SprintVM GetSprintVMBySprintID(int sprintID);
        SprintVM GetSprintVMByFeatureID(string featureID);
        List<SprintVM> GetSprintVMsByProjectID(string projectID);
        bool AddSprint(Sprint sprint);
        bool EditSprint(Sprint newSprint, Sprint oldSprint);
        bool CancelSprint(int sprintID);
    }
}
