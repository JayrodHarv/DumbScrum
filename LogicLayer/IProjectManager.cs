using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public interface IProjectManager {
        Project GetProjectByProjectID(string projectID);
        List<Project> GetProjectsByUserID(int userID);
        List<Project> GetAllProjects();
    }
}
