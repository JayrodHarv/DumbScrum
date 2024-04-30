using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects {
    public class ProjectRole {
        public string ProjectRoleID { get; set; }
        public string ProjectID { get; set; }
        public bool FeaturePrivileges { get; set; }
        public bool UserStoryPrivileges { get; set; }
        public bool SprintPlanningPrivileges { get; set; }
        public bool FeedMessagingPrivileges { get; set; }
        public bool TaskPrivileges { get; set; }
        public bool TaskReviewingPrivileges { get; set; }
        public bool ProjectManagementPrivileges { get; set; }
        public string Description { get; set; }
    }

    public class ProjectRoleVM : ProjectRole {

    }

    public class ProjectRoleListVM {
        public string ProjectRoleID { get; set; }
        public string Description { get; set; }
        public int MembersWithRole { get; set; }
    }
}
