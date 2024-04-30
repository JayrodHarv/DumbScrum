using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public interface IProjectRoleManager {
        List<ProjectRoleListVM> GetProjectRoles(string projectID);
        ProjectRoleVM GetProjectRole(string projectRoleID);
        bool AddProjectRole(ProjectRole projectRole);
        bool EditProjectRole(ProjectRole projectRole);
        bool RemoveProjectRole(string projectRoleID);
    }
}
