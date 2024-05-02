using DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DumbScrumWebMVC.Models {
    public class CreateProjectMemberVM {
        [Required]
        public string ProjectID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int ProjectRoleID { get; set; }
        public List<UserVM> Users { get; set; }
        public List<ProjectRoleListVM> ProjectRoles { get; set; }
    }
}