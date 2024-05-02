using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DumbScrumWebMVC.Models {
    public class ProjectRolesVM {
        public string ProjectID { get; set; }
        public List<ProjectRoleListVM> ProjectRoles { get; set; }
    }
}