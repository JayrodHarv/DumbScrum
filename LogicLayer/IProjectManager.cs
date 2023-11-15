using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public interface IProjectManager {
        ProjectVM GetProjectVMByProjectID(string projectID);
        List<Project> GetProjectsByUserID(int userID);
        List<Project> GetAllProjects();
        bool AddProject(string projectID, string projectOwner, string description, int userID);
    }
}
