using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces {
    public interface IProjectMemberAccessor {
        List<ProjectMemberListVM> SelectProjectMembers(string projectID);
        ProjectMemberVM SelectProjectMember(int userID, string projectID);
        int InsertProjectMember(int userID, string projectID, string projectRoleID);
        int MemberLeaveProject(int userID, string projectID);
        int UpdateMemberRole(int userID, string projectID, string projectRoleID);
    }
}
