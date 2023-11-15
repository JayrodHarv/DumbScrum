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
    public class ProjectManager : IProjectManager {
        private IProjectAccessor _projectAccessor;

        public ProjectManager() {
            _projectAccessor = new ProjectAccessor();
        }

        public ProjectManager(IProjectAccessor projectAccessor) {
            _projectAccessor = projectAccessor;
        }

        public List<Project> GetAllProjects() {
            List<Project> result = new List<Project>();
            try {
                result = _projectAccessor.SelectAllProjects();
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public ProjectVM GetProjectVMByProjectID(string projectID) {
            ProjectVM result = new ProjectVM();
            try {
                result = _projectAccessor.SelectProjectVMByProjectID(projectID);
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public List<Project> GetProjectsByUserID(int userID) {
            List<Project> result = new List<Project>();
            try {
                result = _projectAccessor.SelectProjectsByUserID(userID);
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public bool AddProject(string projectID, string projectOwner, string description, int userID) {
            bool result = false;

            try {
                _projectAccessor.CreateProject(projectID, projectOwner, description, userID);
                result = true;
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }
    }
}
