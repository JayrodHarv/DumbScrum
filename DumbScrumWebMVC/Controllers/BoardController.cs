using DataObjects;
using DumbScrumWebMVC.Models;
using LogicLayer;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.UI;

namespace DumbScrumWebMVC.Controllers
{
    public class BoardController : Controller
    {
        SprintManager sprintManager = new SprintManager();
        TaskManager taskManager = new TaskManager();
        // GET: Board
        public ActionResult Index()
        {
            BoardVM boardVM = new BoardVM();
            try {
                boardVM = GetBoard(0);

            } catch (Exception ex) {
                ViewBag.Error = "Something went wrong while trying to retrieve data for board.\n" + ex.Message;
            }
            return View(boardVM);
        }

        [HttpPost]
        public ActionResult Index(string sprintFilter) {
            BoardVM boardVM = new BoardVM();

            try {
                boardVM = GetBoard(Convert.ToInt32(sprintFilter));
            } catch (Exception ex) {
                ViewBag.Error = "Something went wrong while trying to retrieve data for board.\n" + ex.Message;
            }

            return View(boardVM);
        }

        private BoardVM GetBoard(int sprintID) {
            BoardVM boardVM = new BoardVM();

            try {
                ProjectVM project = (ProjectVM)Session["CurrentProject"];
                boardVM.Sprints = sprintManager.GetSprintVMsByProjectID(project.ProjectID);

                // select the current sprint by checking if the current date is in-between the start and end date of a sprint
                if (sprintID == 0) {
                    DateTime now = DateTime.Now;
                    foreach (SprintVM s in boardVM.Sprints) {
                        if (now > s.StartDate && now <= s.EndDate) {
                            boardVM.CurrentSprint = s;
                        }
                    }
                } else {
                    boardVM.CurrentSprint = sprintManager.GetSprintVMBySprintID(sprintID);
                }

                List<SelectListItem> sprints = new List<SelectListItem>();
                foreach (SprintVM sprint in boardVM.Sprints) {
                    sprints.Add(new SelectListItem { Text = sprint.Name, Value = sprint.SprintID.ToString() });
                }
                boardVM.SprintDropdownItems = sprints;

                List<TaskVM> tasks = new List<TaskVM>();
                if (boardVM.CurrentSprint != null) {
                    tasks = taskManager.GetSprintTaskVMs(boardVM.CurrentSprint.SprintID);
                }
                boardVM.ToDoTasks = tasks.Where(t => t.Status == "To Do");
                boardVM.InProgressTasks = tasks.Where(t => t.Status == "In Progress");
                boardVM.NeedsReviewedTasks = tasks.Where(t => t.Status == "Needs Reviewed");
                boardVM.CompleteTasks = tasks.Where(t => t.Status == "Complete");
            } catch (Exception) {
                throw;
            }

            return boardVM;
        }

        public ActionResult ClaimTask(string taskID) {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(User.Identity.GetUserId());

            try {
                taskManager.UpdateTaskUserID(Convert.ToInt32(taskID), (int)user.UserID);
            } catch (Exception ex) {
                ViewBag.Error = "Failed to claim task, please try again.\n" + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
