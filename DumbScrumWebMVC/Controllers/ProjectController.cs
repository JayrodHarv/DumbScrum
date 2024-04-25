using DataObjects;
using DumbScrumWebMVC.Models;
using LogicLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Controllers
{
    public class ProjectController : Controller {
        ProjectManager _projectManager = new ProjectManager();
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult MyProjects() {
            List<ProjectVM> projects = new List<ProjectVM>();
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(User.Identity.GetUserId());
            ViewBag.Message = "My Projects";
            try {
                // throw new ApplicationException();
                projects = _projectManager.GetProjectsByUserID((int)user.UserID);
            } catch (Exception ex) {
                // we need to go to an error page
                ViewBag.Error = ex.Message;
            }
            return View(projects);
        }

        public ActionResult AllProjects() {
            List<ProjectVM> projects = null;
            try {
                projects = _projectManager.GetAllProjects();
            } catch (Exception ex) {
                ViewBag.Error = ex.Message;
            }
            return View(projects);
        }

        [HttpGet]
        public ActionResult ViewProject(string projectID)
        {
            ProjectVM projectVM = null;
            try {
                projectVM = _projectManager.GetProjectVMByProjectID(projectID);
                Session["CurrentProject"] = projectVM;
            } catch (Exception ex) {
                ViewBag.Error = ex.Message;
            }
            return View("Project", projectVM);
        }

        [Authorize]
        public ActionResult JoinProject(string projectID) {
            try {
                ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = userManager.FindById(User.Identity.GetUserId());
                bool result = _projectManager.JoinProject(projectID, (int)user.UserID);
                if (!result) {
                    ViewBag.Error = "Something went wrong while trying to add you to this project.";
                }
            } catch (Exception ex) {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("MyProjects");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(Project project)
        {
            try
            {
                ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = userManager.FindById(User.Identity.GetUserId());
                project.UserID = (int)user.UserID;
                project.DateCreated = DateTime.Now;
                _projectManager.AddProject(project);
                return RedirectToAction("MyProjects");
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
