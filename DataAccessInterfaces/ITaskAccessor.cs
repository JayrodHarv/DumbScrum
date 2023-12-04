using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces {
    public interface ITaskAccessor {
        int InsertTask(DataObjects.Task task);
        int UpdateTaskUserID(int taskID, int userID);
        int UpdateTaskStatus(int taskID, string status);
        TaskVM SelectTaskByTaskID(int taskID);
        List<TaskVM> SelectSprintTaskVMsByStatus(int sprintID, string status);
        List<TaskVM> SelectTaskVMsByUserID(int userID);
    }
}
