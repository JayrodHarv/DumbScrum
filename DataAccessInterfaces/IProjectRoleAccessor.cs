using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces {
    public interface IProjectRoleAccessor {
        List<ProjectRoleListVM> SelectProjectRoles(string projectID);
        ProjectRoleVM SelectProjectRole(int projectRoleID);
        int InsertProjectRole(ProjectRole projectRole);
        int UpdateProjectRole(ProjectRole projectRole);
        int DeleteProjectRole(int projectRoleID);
    }
}
