using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public interface ITaskManager {
        bool CreateTask(DataObjects.Task task);
        TaskVM GetTask(int taskID);
        List<TaskVM> GetSprintTaskVMsByStatus(int sprintID, string status);
        List<TaskVM> GetTaskVMsByUserID(int userID);
    }
}
