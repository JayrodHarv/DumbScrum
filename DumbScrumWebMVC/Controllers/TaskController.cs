using DumbScrumWebMVC.Models;
using LogicLayer;
using System;
using System.IO;
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

        private string GetActionNameFromFileType(string fileType) {
            string actionName = "";
            switch(fileType) {
                case "Use Case":
                    actionName = "UseCases";
                    break;
                case "Stored Procedure Specification":
                    actionName = "StoredProcedures";
                    break;
                case "User Interface":
                    actionName = "UserInterfaces";
                    break;
                case "ER Diagram":
                    actionName = "ERDiagram";
                    break;
                case "Data Dictionary":
                    actionName = "DataDictionary";
                    break;
                case "Data Model":
                    actionName = "DataModels";
                    break;
            }
            return actionName;
        }

        [HttpGet]
        public ActionResult UseCases(string projectID, int taskID) {
            ViewBag.Tab = "Board";
            string fileType = "Use Case";
            TaskFilesVM taskFilesVM = new TaskFilesVM();
            taskFilesVM.ProjectID = projectID;
            try {
                taskFilesVM.Task = _manager.TaskManager.GetTask(taskID);
                taskFilesVM.Files = _manager.FileManager.GetTaskFilesByType(taskID, fileType);
                taskFilesVM.FileType = fileType;
            } catch (Exception ex) {
                TempData["Error"] = "Unable to retrieve use cases.\n" + ex.Message;
            }
            return View(taskFilesVM);
        }

        [HttpGet]
        public ActionResult UserInterfaces(string projectID, int taskID) {
            ViewBag.Tab = "Board";
            string fileType = "User Interface";
            TaskFilesVM taskFilesVM = new TaskFilesVM();
            taskFilesVM.ProjectID = projectID;
            try {
                taskFilesVM.Task = _manager.TaskManager.GetTask(taskID);
                taskFilesVM.Files = _manager.FileManager.GetTaskFilesByType(taskID, fileType);
                taskFilesVM.FileType = fileType;
            } catch (Exception ex) {
                TempData["Error"] = "Unable to retrieve user interfaces.\n" + ex.Message;
            }
            return View(taskFilesVM);
        }

        [HttpGet]
        public ActionResult ERDiagram(string projectID, int taskID) {
            ViewBag.Tab = "Board";
            string fileType = "ER Diagram";
            TaskFilesVM taskFilesVM = new TaskFilesVM();
            taskFilesVM.ProjectID = projectID;
            try {
                taskFilesVM.Task = _manager.TaskManager.GetTask(taskID);
                taskFilesVM.Files = _manager.FileManager.GetTaskFilesByType(taskID, fileType);
                taskFilesVM.FileType = fileType;
            } catch (Exception ex) {
                TempData["Error"] = "Unable to retrieve ER Diagrams.\n" + ex.Message;
            }
            return View(taskFilesVM);
        }

        [HttpPost]
        public ActionResult UploadTaskFile(string projectID, int taskID, string fileType, HttpPostedFileBase file) {
            DataObjects.File file1 = null;
            if (file != null) {
                try {
                    using (Stream stream = file.InputStream) {
                        byte[] data = new byte[file.ContentLength];
                        stream.Read(data, 0, file.ContentLength);
                        file1 = new DataObjects.File {
                            Data = data,
                            Extension = file.ContentType,
                            TaskID = taskID,
                            FileName = file.FileName,
                            Type = fileType,
                            LastEdited = DateTime.Now
                        };
                    }

                    if(_manager.FileManager.AddTaskFile(file1)) {
                        TempData["Success"] = "Successfully uploaded file";
                    } else {
                        TempData["Warning"] = "Something went wrong while uploading file. Please try again";
                    }
                } catch (Exception ex) {
                    TempData["Error"] = "Something went wrong while uploading file:\n" + ex.Message;
                }
            } else {
                TempData["Warning"] = "You must select a file to upload";
            }

            return RedirectToAction(GetActionNameFromFileType(fileType), new { projectID, taskID });
        }

        [HttpPost]
        public ActionResult DeleteTaskFile(string projectID, int taskID, int fileID) {
            try {
                if(_manager.FileManager.RemoveFile(fileID)) {
                    TempData["Success"] = "Successfully deleted file";
                } else {
                    TempData["Warning"] = "Something went wrong while deleting file. Please try again";
                }
            } catch (Exception ex) {
                TempData["Warning"] = "Something went wrong while deleting file:\n" + ex.Message;
            }
            return RedirectToAction("UseCases", new { projectID, taskID });
        }

        [HttpGet]
        public ActionResult DownloadTaskFile(int fileID) {
            try {
                DataObjects.File file = _manager.FileManager.GetTaskFile(fileID);
                if (file != null) {
                    return File(file.Data, file.Extension, file.FileName);
                } else {
                    TempData[""] = "Something went wrong while retrieving file. Please try again";
                }
            } catch (Exception ex) {
                TempData[""] = "Something went wrong while retrieving file:\n" + ex.Message;
            }
            return null;
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