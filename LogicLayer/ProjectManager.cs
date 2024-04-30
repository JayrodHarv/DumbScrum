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

        public List<ProjectVM> GetAllProjects() {
            List<ProjectVM> result = new List<ProjectVM>();
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

        public List<ProjectVM> GetProjectsByUserID(int userID) {
            List<ProjectVM> result = new List<ProjectVM>();
            try {
                result = _projectAccessor.SelectProjectsByUserID(userID);
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public bool AddProject(Project project) {
            bool result = false;
            try {
                result = (2 == _projectAccessor.CreateProject(project));
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public bool RemoveProject(string projectID) {
            bool result = false;
            try {
                result = (1 == _projectAccessor.DeleteProject(projectID));
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }
    }
}
