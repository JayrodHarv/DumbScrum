using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LogicLayer {
    public class TaskManager : ITaskManager {

        private ITaskAccessor _taskAccessor = null;

        public TaskManager() {
            _taskAccessor = new TaskAccessor();
        }

        public TaskManager(ITaskAccessor taskAccessor) {
            _taskAccessor = taskAccessor;
        }

        public bool CreateTask(DataObjects.Task task) {
            bool result = false;
            try {
                if (1 == _taskAccessor.InsertTask(task)) {
                    result = true;
                }
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public List<TaskVM> GetSprintTaskVMs(int sprintID) {
            List<TaskVM> result = new List<TaskVM>();
            try {
                result = _taskAccessor.SelectSprintTaskVMs(sprintID);
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public TaskVM GetTask(int taskID) {
            TaskVM result = new TaskVM();
            try {
                result = _taskAccessor.SelectTaskByTaskID(taskID);
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public List<TaskVM> GetTaskVMsByUserID(int userID) {
            List<TaskVM> result = new List<TaskVM>();
            try {
                result = _taskAccessor.SelectTaskVMsByUserID(userID);
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public bool UpdateTaskStatus(int taskID, string status) {
            bool result = false;
            try {
                result = (1 == _taskAccessor.UpdateTaskStatus(taskID, status));
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public bool UpdateTaskUserID(int taskID, int userID) {
            bool result = false;
            try {
                result = (1 == _taskAccessor.UpdateTaskUserID(taskID, userID));
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }
    }
}
