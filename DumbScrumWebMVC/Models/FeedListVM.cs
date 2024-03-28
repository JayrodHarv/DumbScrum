using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Models {
    public class FeedListVM {
        public List<SprintVM> Sprints { get; set; }
        public List<FeedMessageVM> FeedMessages { get; set; }
        public SprintVM CurrentSprint { get; set; }
        public List<SelectListItem> SprintDropdownItems { get; set; }
    }
}