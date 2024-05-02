using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Models {
    public class BoardVM {
        public string ProjectID { get; set; }
        public int SprintID { get; set; }
        public SprintVM CurrentSprint { get; set; }
        public List<SprintVM> Sprints { get; set; }
        public List<TaskVM> ToDoTasks { get; set; }
        public List<TaskVM> InProgressTasks { get; set; }
        public List<TaskVM> NeedsReviewedTasks { get; set; }
        public List<TaskVM> CompleteTasks { get; set; }
    }
}