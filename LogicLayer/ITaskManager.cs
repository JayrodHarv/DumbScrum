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
        bool UpdateTaskUserID(int taskID, int userID);
        bool UpdateTaskStatus(int taskID, string status);
        List<TaskVM> GetSprintTaskVMs(int sprintID);
        List<TaskVM> GetTaskVMsByUserID(int userID);
    }
}
