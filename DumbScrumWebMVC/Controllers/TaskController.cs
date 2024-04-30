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
        public ActionResult Overview(int taskID) {
            ViewBag.Tab = "Overview";
            TaskVM taskVM = new TaskVM();
            try {
                taskVM = _manager.TaskManager.GetTask(taskID);
            } catch (Exception ex) {
                TempData["Error"] = "Unable to retrieve task.\n" + ex.Message;
            }
            return View(taskVM);
        }

        [HttpGet]
        public ActionResult UseCases(int taskID) {
            ViewBag.Tab = "Use Cases";
            UseCasesVM useCasesVM = new UseCasesVM();
            useCasesVM.TaskID = taskID;
            try {
                useCasesVM.Task = _manager.TaskManager.GetTask(taskID);
                useCasesVM.Files = _manager.FileManager.GetTaskFilesByType(taskID, "Use Case");
            } catch (Exception ex) {
                TempData["Error"] = "Unable to retrieve use cases.\n" + ex.Message;
            }
            return View(useCasesVM);
        }
    }
}