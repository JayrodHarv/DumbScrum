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

namespace DumbScrumWebMVC.Controllers {
    public class BoardController : Controller {
        MainManager _manager;

        public BoardController() {
            _manager = MainManager.GetMainManager();
        }

        [HttpGet]
        public ActionResult Index(string projectID) {
            ViewBag.Tab = "Board";
            BoardVM boardVM = new BoardVM();
            boardVM.ProjectID = projectID;
            try {
                boardVM = GetBoard(projectID, 0);
            } catch (Exception ex) {
                TempData["Error"] = "Something went wrong while trying to retrieve data for board.\n" + ex.Message;
            }
            return View(boardVM);
        }

        [HttpPost]
        public ActionResult Index(string projectID, string sprintID) {
            ViewBag.Tab = "Board";
            BoardVM boardVM = new BoardVM();
            boardVM.ProjectID = projectID;
            try {
                boardVM = GetBoard(projectID, Convert.ToInt32(sprintID));
            } catch (Exception ex) {
                TempData["Error"] = "Something went wrong while trying to retrieve data for board.\n" + ex.Message;
            }

            return View(boardVM);
        }

        private BoardVM GetBoard(string projectID, int sprintID) {
            BoardVM boardVM = new BoardVM();
            boardVM.ProjectID = projectID;
            List<TaskVM> tasks = new List<TaskVM>();
            try {
                boardVM.Sprints = _manager.SprintManager.GetSprintVMsByProjectID(projectID);

                if(boardVM.Sprints.Count > 0) {
                    // select the current sprint by checking if the current date is in-between the start and end date of a sprint
                    if (sprintID == 0) {
                        DateTime now = DateTime.Now;
                        foreach (SprintVM s in boardVM.Sprints) {
                            if (now > s.StartDate && now <= s.EndDate) {
                                boardVM.CurrentSprint = s;
                            }
                        }
                        if(boardVM.CurrentSprint == null) {
                            boardVM.CurrentSprint = boardVM.Sprints[0];
                        }
                    } else {
                        boardVM.CurrentSprint = _manager.SprintManager.GetSprintVMBySprintID(sprintID);
                    }

                    if (boardVM.CurrentSprint != null) {
                        tasks = _manager.TaskManager.GetSprintTaskVMs(boardVM.CurrentSprint.SprintID);
                    }

                    if(tasks.Count == 0) {
                        TempData["Error"] = "Failed to retreive tasks for this sprint.";
                    }
                }

                boardVM.ToDoTasks = tasks.Where(t => t.Status == "To Do").ToList();
                boardVM.InProgressTasks = tasks.Where(t => t.Status == "In Progress").ToList();
                boardVM.NeedsReviewedTasks = tasks.Where(t => t.Status == "Needs Reviewed").ToList();
                boardVM.CompleteTasks = tasks.Where(t => t.Status == "Complete").ToList();
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }

            return boardVM;
        }

        [HttpGet]
        public ActionResult ClaimTask(string projectID, int taskID) {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(User.Identity.GetUserId());

            try {
                _manager.TaskManager.UpdateTaskUserID(taskID, (int)user.UserID);
            } catch (Exception ex) {
                TempData["Error"] = "Failed to claim task, please try again.\n" + ex.Message;
            }
            return RedirectToAction("Index", "Board", new { projectID = projectID });
        }
    }
}
