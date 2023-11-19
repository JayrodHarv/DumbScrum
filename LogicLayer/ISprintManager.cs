using DataObjects;
using System.Collections.Generic;

namespace LogicLayer {
    public interface ISprintManager {
        SprintVM GetSprintVMBySprintID(int sprintID);
        List<Sprint> GetSprintsByProjectID(string projectID);
        bool AddSprint(Sprint sprint);
    }
}
