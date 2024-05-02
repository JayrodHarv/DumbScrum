﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects {
    public class ProjectRole {
        [Required]
        public int ProjectRoleID { get; set; }
        [Required] 
        public string RoleName { get; set; }
        [Required]
        public string ProjectID { get; set; }
        [Required]
        public bool FeaturePrivileges { get; set; }
        [Required]
        public bool UserStoryPrivileges { get; set; }
        [Required]
        public bool SprintPlanningPrivileges { get; set; }
        [Required]
        public bool FeedMessagingPrivileges { get; set; }
        [Required]
        public bool TaskPrivileges { get; set; }
        [Required]
        public bool TaskReviewingPrivileges { get; set; }
        [Required]
        public bool ProjectManagementPrivileges { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class ProjectRoleVM : ProjectRole {

    }

    public class ProjectRoleListVM {
        public int ProjectRoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int MembersWithRole { get; set; }
    }
}
