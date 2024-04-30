using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Models {
    public class BacklogVM {
        public string ProjectID { get; set; }
        public List<FeatureVM> Features { get; set; }
        public string SelectedFeatureID { get; set; }
        public List<UserStory> UserStories { get; set; }
    }
}