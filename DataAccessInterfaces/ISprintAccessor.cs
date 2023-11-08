using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces {
    public interface ISprintAccessor {
        SprintVM SelectSprintVMBySprintID(int sprintID);
        List<Sprint> SelectSprintsByProjectID(string projectID);
    }
}
