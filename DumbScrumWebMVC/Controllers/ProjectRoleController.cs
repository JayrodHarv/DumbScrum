using DataObjects;
using DumbScrumWebMVC.Models;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Controllers {
    public class ProjectRoleController : Controller {
        MainManager _manager;

        public ProjectRoleController() {
            _manager = MainManager.GetMainManager();
        }

        [HttpGet]
        public ActionResult ProjectRoles(string projectID) {
            ViewBag.Tab = "Manage";
            ProjectRolesVM projectRolesVM = new ProjectRolesVM();
            projectRolesVM.ProjectID = projectID;
            try {
                projectRolesVM.ProjectRoles = _manager.ProjectRoleManager.GetProjectRoles(projectID);
            } catch (Exception ex) {
                TempData["Error"] = "Something went wrong while trying to retreive the project roles for this project.\n" + ex.Message;
            }
            return View(projectRolesVM);
        }

        [HttpGet]
        public ActionResult Create(string projectID) {
            ProjectRole projectRole = new ProjectRole();
            projectRole.ProjectID = projectID;
            return View(projectRole);
        }

        [HttpPost]
        public ActionResult Create(ProjectRole projectRole) {
            try {
                if (_manager.ProjectRoleManager.AddProjectRole(projectRole)) {
                    TempData["Success"] = "Successfully added new project role.";
                    return RedirectToAction("ProjectRoles", new { projectID = projectRole.ProjectID });
                } else {
                    TempData["Warning"] = "Something went wrong while trying to add new role.";
                }
            } catch (Exception ex) {
                TempData["Error"] = "Something went wrong while trying to add new role." + ex.Message;
            }
            return View(projectRole);
        }

        [HttpGet]
        public ActionResult Edit(string projectID, int projectRoleID) {
            ProjectRole projectRole = null;
            try {
                projectRole = _manager.ProjectRoleManager.GetProjectRole(projectRoleID);
                if(projectRole == null) {
                    TempData["Warning"] = "Was not able to retreive project role data";
                    return RedirectToAction("Index", "ProjectRole", new { projectID });
                }
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return View(projectRole);
        }

        [HttpPost]
        public ActionResult Edit(ProjectRole projectRole) {
            try {
                if (_manager.ProjectRoleManager.EditProjectRole(projectRole)) {
                    TempData["Success"] = "Successfully added new project role.";
                    return RedirectToAction("ProjectRoles", new { projectID = projectRole.ProjectID });
                } else {
                    TempData["Warning"] = "Something went wrong while trying to add new role.";
                }
            } catch (Exception ex) {
                TempData["Error"] = "Something went wrong while trying to add new role." + ex.Message;
            }
            return View(projectRole);
        }

        [HttpPost]
        public ActionResult Delete(string projectID, string projectRoleID) {
            try {
                if(_manager.ProjectRoleManager.RemoveProjectRole(Convert.ToInt32(projectID))) {
                    TempData["Success"] = "Successfully removed the role: " + projectRoleID;
                } else {
                    TempData["Warning"] = "Something went wrong while trying to remove role: " + projectRoleID;
                }
            } catch (Exception ex) {
                TempData["Error"] = "Failed to remove role.\n" + ex.Message;
            }
            return RedirectToAction("ProjectRoles", new { projectID = projectID });
        }
    }
}