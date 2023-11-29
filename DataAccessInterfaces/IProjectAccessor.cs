using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces {
    public interface IProjectAccessor {
        ProjectVM SelectProjectVMByProjectID(string projectID);
        List<ProjectVM> SelectProjectsByUserID(int userID);
        List<ProjectVM> SelectAllProjects();
        int CreateProject(Project project);
        int JoinProject(string projectID, int userID);
        int LeaveProject(int userID, string projectID);
    }
}
