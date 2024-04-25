using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Models {
    public class BoardVM {
        public SprintVM CurrentSprint { get; set; }
        public IEnumerable<SprintVM> Sprints { get; set; } = Enumerable.Empty<SprintVM>();
        public IEnumerable<TaskVM> ToDoTasks { get; set; } = Enumerable.Empty<TaskVM>();
        public IEnumerable<TaskVM> InProgressTasks { get; set; } = Enumerable.Empty<TaskVM>();
        public IEnumerable<TaskVM> NeedsReviewedTasks { get; set; } = Enumerable.Empty<TaskVM>();
        public IEnumerable<TaskVM> CompleteTasks { get; set; } = Enumerable.Empty<TaskVM>();
        public List<SelectListItem> SprintDropdownItems { get; set; }
    }
}