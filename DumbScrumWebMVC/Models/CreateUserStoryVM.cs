using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DumbScrumWebMVC.Models {
    public class CreateUserStoryVM {
        public string ProjectID { get; set; }
        public string FeatureID { get; set; }
        public string Person { get; set; }
        public string Action { get; set; }
        public string Reason { get; set; }
    }
}