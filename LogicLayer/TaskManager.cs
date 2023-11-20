using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool CreateTask(int sprintID, int storyID, string status) {
            bool result = false;
            try {
                if (1 == _taskAccessor.InsertTask(sprintID, storyID, status)) {
                    result = true;
                }
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public List<TaskVM> GetTaskVMsBySprintID(int sprintID) {
            List<TaskVM> result = new List<TaskVM>();
            try {
                result = _taskAccessor.SelectTaskVMsBySprintID(sprintID);
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
    }
}
