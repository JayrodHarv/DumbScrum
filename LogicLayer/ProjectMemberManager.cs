using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public class ProjectMemberManager : IProjectMemberManager {
        private IProjectMemberAccessor _projectMemberAccessor;

        public ProjectMemberManager() {
            _projectMemberAccessor = new ProjectMemberAccessor();
        }

        public ProjectMemberManager(IProjectMemberAccessor projectMemberAccessor) {
            _projectMemberAccessor = projectMemberAccessor;
        }

        public List<ProjectMemberListVM> GetProjectMembers(string projectID) {
            List<ProjectMemberListVM> members = new List<ProjectMemberListVM>();
            try {
                members = _projectMemberAccessor.SelectProjectMembers(projectID);
            } catch (Exception ex) {
                throw ex;
            }
            return members;
        }

        public ProjectMemberVM GetProjectMember(int userID, string projectID) {
            ProjectMemberVM projectMemberVM;
            try {
                projectMemberVM = _projectMemberAccessor.SelectProjectMember(userID, projectID);
            } catch (Exception ex) {
                throw ex;
            }
            return projectMemberVM;
        }

        public bool AddProjectMember(int userID, string projectID, int projectRoleID) {
            try {
                return 0 < _projectMemberAccessor.InsertProjectMember(userID, projectID, projectRoleID);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool EditMemberRole(int userID, string projectID, int projectRoleID) {
            try {
                return 0 < _projectMemberAccessor.UpdateMemberRole(userID, projectID, projectRoleID);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool MemberLeaveProject(int userID, string projectID) {
            try {
                return 0 < _projectMemberAccessor.MemberLeaveProject(userID, projectID);
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
