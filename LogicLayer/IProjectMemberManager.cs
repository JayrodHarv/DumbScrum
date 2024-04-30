using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public interface IProjectMemberManager {
        List<ProjectMemberListVM> GetProjectMembers(string projectID);
        ProjectMemberVM GetProjectMember(int userID, string projectID);
        bool AddProjectMember(int userID, string projectID, string projectRoleID);
        bool MemberLeaveProject(int userID, string projectID);
        bool EditMemberRole(int userID, string projectID, string projectRoleID);
    }
}
