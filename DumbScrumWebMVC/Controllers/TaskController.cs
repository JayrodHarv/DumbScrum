using DataObjects;
using DumbScrumWebMVC.Models;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Controllers {
    public class TaskController : Controller {
        MainManager _manager;

        public TaskController() {
            _manager = MainManager.GetMainManager();
        }

        [HttpGet]
        public ActionResult Overview(string projectID, int taskID) {
            ViewBag.Tab = "Board";
            TaskOverviewVM taskOverviewVM = new TaskOverviewVM();
            taskOverviewVM.ProjectID = projectID;
            try {
                taskOverviewVM.Task = _manager.TaskManager.GetTask(taskID);
            } catch (Exception ex) {
                TempData["Error"] = "Unable to retrieve task.\n" + ex.Message;
            }
            return View(taskOverviewVM);
        }

        [HttpGet]
        public ActionResult UseCases(string projectID, int taskID) {
            ViewBag.Tab = "Board";
            UseCasesVM useCasesVM = new UseCasesVM();
            useCasesVM.ProjectID = projectID;
            useCasesVM.TaskID = taskID;
            try {
                useCasesVM.Task = _manager.TaskManager.GetTask(taskID);
                useCasesVM.Files = _manager.FileManager.GetTaskFilesByType(taskID, "Use Case");
            } catch (Exception ex) {
                TempData["Error"] = "Unable to retrieve use cases.\n" + ex.Message;
            }
            return View(useCasesVM);
        }

        [HttpPost]
        public ActionResult SendToBeReviewed(string projectID, int taskID) {
            try {
                if(_manager.TaskManager.UpdateTaskStatus(taskID, "Needs Reviewed")) {
                    TempData["Success"] = "Successfully sent task to be reviewed";
                } else {
                    TempData["Warning"] = "Something went wrong while sending task to be reviewed";
                }
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Overview", "Task", new { projectID, taskID });
        }
    }
}