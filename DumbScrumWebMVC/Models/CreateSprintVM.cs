using DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Models {
    public class CreateSprintVM {
        public string ProjectID { get; set; }
        public List<FeatureVM> Features { get; set; }
        [Required]
        public string FeatureID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}