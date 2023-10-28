using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces {
    public interface IProjectAccessor {
        Project SelectProjectByProjectID(string projectID);
        List<Project> SelectProjectsByUserID(string userID);
        List<Project> SelectAllProjects();
    }
}
