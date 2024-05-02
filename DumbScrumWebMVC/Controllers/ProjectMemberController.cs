using DataObjects;
using DumbScrumWebMVC.Models;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Controllers {
    public class ProjectMemberController : Controller {
        MainManager _manager;

        public ProjectMemberController() {
            _manager = MainManager.GetMainManager();
        }

        [HttpGet]
        [Authorize]
        public ActionResult ProjectMembers(string projectID) {
            ViewBag.Tab = "Manage";
            ProjectMemberVM user = (ProjectMemberVM)Session["ProjectMember"];
            if (!user.ProjectRole.ProjectManagementPrivileges) {
                TempData["Warning"] = "You don't have access to view this page. Please contact your project owner if you think this is a mistake.";
                return RedirectToAction("Index", "Feed", new { projectID = projectID });
            }

            ProjectMembersVM projectMembersVM = new ProjectMembersVM();
            projectMembersVM.ProjectID = projectID;
            try {
                projectMembersVM.Members = _manager.ProjectMemberManager.GetProjectMembers(projectID);
            } catch (Exception ex) {
                TempData["Error"] = "Something went wrong while trying to retreive the project members.\n" + ex.Message;
            }
            return View(projectMembersVM);
        }

        [HttpGet]
        public ActionResult Create(string projectID) {
            CreateProjectMemberVM createProjectMemberVM = new CreateProjectMemberVM();
            createProjectMemberVM.ProjectID = projectID;
            try {
                createProjectMemberVM.ProjectRoles = _manager.ProjectRoleManager.GetProjectRoles(projectID);
                createProjectMemberVM.Users = _manager.UserManager.GetAllUsers();
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return View(createProjectMemberVM);
        }

        [HttpPost]
        public ActionResult Create(CreateProjectMemberVM createProjectMemberVM) {
            try {
                ProjectMemberVM projectMember = _manager.ProjectMemberManager.GetProjectMember(createProjectMemberVM.UserID, createProjectMemberVM.ProjectID);

                if(projectMember != null) {
                    TempData["Warning"] = "This user is already a project member";
                } else {
                    if (_manager.ProjectMemberManager.AddProjectMember(createProjectMemberVM.UserID, createProjectMemberVM.ProjectID, createProjectMemberVM.ProjectRoleID)) {
                        TempData["Success"] = "Successfully added new member to " + createProjectMemberVM.ProjectID;
                        return RedirectToAction("ProjectMembers", new { projectID = createProjectMemberVM.ProjectID });
                    } else {
                        TempData["Warning"] = "Something went wrong while trying to add new member to project";
                    }
                }
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            try {
                createProjectMemberVM.ProjectRoles = _manager.ProjectRoleManager.GetProjectRoles(createProjectMemberVM.ProjectID);
                createProjectMemberVM.Users = _manager.UserManager.GetAllUsers();
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return View(createProjectMemberVM);
        }

        [HttpPost]
        public ActionResult KickMember(string projectID, int userID) {
            try {
                if(_manager.ProjectMemberManager.MemberLeaveProject(userID, projectID)) {
                    TempData["Success"] = "Member has successfully been kicked from project";
                } else {
                    TempData["Warning"] = "Something went wrong while trying to kick member from project";
                }
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ProjectMembers", new { projectID = projectID });
        }
    }
}