using DataObjects;
using System.Collections.Generic;

namespace LogicLayer {
    public interface ISprintManager {
        SprintVM GetSprintVMBySprintID(int sprintID);
        List<SprintVM> GetSprintVMsByProjectID(string projectID);
        bool AddSprint(Sprint sprint);
    }
}
