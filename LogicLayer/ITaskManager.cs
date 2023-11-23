using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public interface ITaskManager {
        bool CreateTask(DataObjects.Task task);
        List<TaskVM> GetTaskVMsBySprintID(int sprintID);
        List<TaskVM> GetTaskVMsByUserID(int userID);
    }
}
