using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces {
    public interface IProjectAccessor {
        ProjectVM SelectProjectVMByProjectID(string projectID);
        List<Project> SelectProjectsByUserID(int userID);
        List<Project> SelectAllProjects();
        int CreateProject(Project project, int userID);
        int LeaveProject(int userID, string projectID);
    }
}
