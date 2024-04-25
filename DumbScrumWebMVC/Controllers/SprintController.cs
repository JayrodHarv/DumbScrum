using DataObjects;
using DumbScrumWebMVC.Models;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.UI;

namespace DumbScrumWebMVC.Controllers
{
    public class SprintController : Controller {
        SprintManager _sprintManager = new SprintManager();
        FeatureManager _featureManager = new FeatureManager();
        UserStoryManager _userStoryManager = new UserStoryManager();
        TaskManager _taskManager = new TaskManager();
        ProjectVM _currentProject;
        // GET: Sprint
        public ActionResult Index() {
            _currentProject = (ProjectVM)Session["CurrentProject"];
            List<SprintVM> sprints = new List<SprintVM>();
            try {
                sprints = _sprintManager.GetSprintVMsByProjectID(_currentProject.ProjectID);
            } catch (Exception) {

                throw;
            }
            return View(sprints);
        }

        // GET: Sprint/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sprint/Create
        public ActionResult Create() {
            _currentProject = (ProjectVM)Session["CurrentProject"];
            List<FeatureVM> _features = _featureManager.GetFeaturesByProjectID(_currentProject.ProjectID);
            List<SelectListItem> FeaturesSelectList = new List<SelectListItem>();
            foreach (FeatureVM f in _features) {
                FeaturesSelectList.Add(new SelectListItem { Text = f.Name, Value = f.FeatureID.ToString() });
            }
            ViewData["Features"] = FeaturesSelectList;
            return View();
        }

        // POST: Sprint/Create
        [HttpPost]
        public ActionResult Create(Sprint inputSprint) {
            _currentProject = Session["CurrentProject"] as ProjectVM;
            try {
                List<SprintVM> sprints = _sprintManager.GetSprintVMsByProjectID(_currentProject.ProjectID);
                foreach (Sprint s in sprints) {
                    if (s.FeatureID == inputSprint.FeatureID) {
                        ViewBag.ErrorMessage = "Sprint already exists for the feature you selected.";
                        List<FeatureVM> _features = _featureManager.GetFeaturesByProjectID(_currentProject.ProjectID);
                        List<SelectListItem> FeaturesSelectList = new List<SelectListItem>();
                        foreach (FeatureVM f in _features) {
                            FeaturesSelectList.Add(new SelectListItem { Text = f.Name, Value = f.FeatureID.ToString() });
                        }
                        ViewData["Features"] = FeaturesSelectList;
                        return View(inputSprint);
                    }
                }
                if(!_sprintManager.AddSprint(inputSprint)) {
                    ViewBag.ErrorMessage = "Something went wrong while trying to plan this sprint.";
                    return View(inputSprint);
                }
                List<UserStory> stories = _userStoryManager.GetFeatureUserStories(inputSprint.FeatureID);
                Sprint sprint = _sprintManager.GetSprintVMByFeatureID(inputSprint.FeatureID);
                foreach (UserStory story in stories) {
                    Task task = new Task() {
                        SprintID = sprint.SprintID,
                        StoryID = story.StoryID,
                        Status = "To Do"
                    };
                    _taskManager.CreateTask(task);
                }
                return RedirectToAction("Index");
            }
            catch {
                throw;
            }
        }

        // GET: Sprint/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sprint/Edit/5
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

        // GET: Sprint/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sprint/Delete/5
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
