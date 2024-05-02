using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Controllers {
    public class FileController : Controller {
        MainManager _manager;

        public FileController() {
            _manager = MainManager.GetMainManager();
        }

        [HttpPost]
        public ActionResult SaveTaskFile(string projectID, int taskID, string fileType) {
            try {
                //File file = new File();
                //if (_manager.FileManager.AddTaskFile()) {
                //    TempData["Success"] = "Successfully added task file";
                //} else {
                //    TempData["Warning"] = "Something went wrong while adding task file";
                //}
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(fileType + "s", new { projectID, taskID });
        }
    }
}