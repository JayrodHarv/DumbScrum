using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Controllers
{
    public class ProjectController : Controller
    {
        ProjectManager _projectManager = new ProjectManager();
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyProjects() {
            List<ProjectVM> projects = new List<ProjectVM>();
            if (Session["LoggedInUser"] != null) {
                User user = (User)Session["LoggedInUser"];
                ViewBag.Message = "My Projects";
                try {
                    // throw new ApplicationException();
                    projects = _projectManager.GetProjectsByUserID(user.UserID);
                } catch (Exception ex) {
                    // we need to go to an error page
                    ViewBag.ErrorMessage = ex.Message;
                    return View("InventoryError");
                }
            }
            return View(projects);
        }

        public ActionResult AllProjects() {
            List<ProjectVM> projects = null;
            try {
                projects = _projectManager.GetAllProjects();
            } catch (Exception ex) {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
            return View(projects);
        }

        // GET: Project/Details/5
        public ActionResult ViewProject(string projectID)
        {
            ProjectVM projectVM;
            try {
                projectVM = _projectManager.GetProjectVMByProjectID(projectID);
                Session["CurrentProject"] = projectVM;
                return View("Project", projectVM);
            } catch (Exception ex) {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        // GET: Project/Create
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
                UserVM user = (UserVM)Session["LoggedInUser"];
                project.UserID = user.UserID;
                project.DateCreated = DateTime.Now;
                _projectManager.AddProject(project);
                return RedirectToAction("MyProjects");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Project/Edit/5
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
