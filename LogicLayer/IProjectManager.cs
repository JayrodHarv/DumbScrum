using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public interface IProjectManager {
        ProjectVM GetProjectVMByProjectID(string projectID);
        List<ProjectVM> GetProjectsByUserID(int userID);
        List<ProjectVM> GetAllProjects();
        bool AddProject(Project project);
        bool RemoveProject(string projectID);
    }
}
