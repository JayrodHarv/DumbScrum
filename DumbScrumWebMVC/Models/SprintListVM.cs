using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DumbScrumWebMVC.Models {
    public class SprintListVM {
        public string ProjectID { get; set; }
        public List<SprintVM> Sprints { get; set; }
    }
}