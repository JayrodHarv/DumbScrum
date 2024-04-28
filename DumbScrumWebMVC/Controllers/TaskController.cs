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
        private TaskManager _taskManager;
        private FileManager _fileManager;

        public TaskController() {
            _taskManager = new TaskManager();
            _fileManager = new FileManager();
        }

        [HttpGet]
        public ActionResult Overview(int taskID) {
            ViewBag.Tab = "Overview";
            TaskVM taskVM = new TaskVM();
            try {
                taskVM = _taskManager.GetTask(taskID);
            } catch (Exception ex) {
                ViewBag.Error = "Unable to retrieve task.\n" + ex.Message;
            }
            return View(taskVM);
        }

        [HttpGet]
        public ActionResult UseCases(int taskID) {
            ViewBag.Tab = "Use Cases";
            UseCasesVM useCasesVM = new UseCasesVM();
            useCasesVM.TaskID = taskID;
            try {
                useCasesVM.Task = _taskManager.GetTask(taskID);
                useCasesVM.Files = _fileManager.GetTaskFilesByType(taskID, "Use Case");
            } catch (Exception ex) {
                ViewBag.Error = "Unable to retrieve use cases.\n" + ex.Message;
            }
            return View(useCasesVM);
        }
    }
}