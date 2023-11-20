using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces {
    public interface ITaskAccessor {
        int InsertTask(int sprintID, int storyID, string status);
        List<TaskVM> SelectTaskVMsBySprintID(int sprintID);
        List<TaskVM> SelectTaskVMsByUserID(int userID);
    }
}
