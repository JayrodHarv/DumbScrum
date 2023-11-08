using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public interface ISprintManager {
        SprintVM GetSprintVMBySprintID(int sprintID);
        List<Sprint> GetSprintsByProjectID(string projectID);
    }
}
