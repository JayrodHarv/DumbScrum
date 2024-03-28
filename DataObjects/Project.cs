using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataObjects {
    public class Project {
        [Required]
        [DisplayName("Project Name")]
        public string ProjectID { get; set; }
        public int UserID { get; set; }
        [Required]
        [DisplayName("Start Date")]
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }

    public class ProjectVM : Project {
        [DisplayName("Project Owner")]
        public string ProjectOwner {  get; set; }
    }
}
