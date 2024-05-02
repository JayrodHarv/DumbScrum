using DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DumbScrumWebMVC.Models {
    public class EditSprintVM {
        public string ProjectID { get; set; }
        [Required]
        public int SprintID { get; set; }
        [Required]
        [DisplayName("Feature")]
        public string FeatureID { get; set; }
        [Required]
        [DisplayName("Sprint Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
    }
}