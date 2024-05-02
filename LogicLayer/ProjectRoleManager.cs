using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public class ProjectRoleManager : IProjectRoleManager {
        private IProjectRoleAccessor _projectRoleAccessor;

        public ProjectRoleManager() {
            _projectRoleAccessor = new ProjectRoleAccessor();
        }

        public ProjectRoleManager(IProjectRoleAccessor projectRoleAccessor) {
            _projectRoleAccessor = projectRoleAccessor;
        }

        public List<ProjectRoleListVM> GetProjectRoles(string projectID) {
            List<ProjectRoleListVM> projectRoles = new List<ProjectRoleListVM>();
            try {
                projectRoles = _projectRoleAccessor.SelectProjectRoles(projectID);
            } catch (Exception ex) {
                throw ex;
            }
            return projectRoles;
        }

        public ProjectRoleVM GetProjectRole(int projectRoleID) {
            ProjectRoleVM projectRole;
            try {
                projectRole = _projectRoleAccessor.SelectProjectRole(projectRoleID);
            } catch (Exception ex) {
                throw ex;
            }
            return projectRole;
        }

        public bool AddProjectRole(ProjectRole projectRole) {
            try {
                return 0 < _projectRoleAccessor.InsertProjectRole(projectRole);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool EditProjectRole(ProjectRole projectRole) {
            try {
                return 0 < _projectRoleAccessor.UpdateProjectRole(projectRole);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool RemoveProjectRole(int projectRoleID) {
            try {
                return 0 < _projectRoleAccessor.DeleteProjectRole(projectRoleID);
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
