using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Project GetProjectByProjectID(string projectID) {
            throw new NotImplementedException();
        }

        public List<Project> GetProjectsByUserID(string userID) {
            throw new NotImplementedException();
        }
    }
}
