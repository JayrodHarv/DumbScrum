using DataObjects;
using DumbScrumWebMVC.Models;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Controllers {
    public class BacklogController : Controller {
        FeatureManager _featureManager = new FeatureManager();
        UserStoryManager _userStoryManager = new UserStoryManager();
        ProjectVM _currentProject;
        BacklogVM _backlogVM = new BacklogVM();
        // GET: Backlog
        public ActionResult Index() {
            _currentProject = (ProjectVM)Session["CurrentProject"];
            _backlogVM.UserStories = new List<UserStory>();
            try {
                _backlogVM.Features = _featureManager.GetFeaturesByProjectID(_currentProject.ProjectID);
            } catch (Exception) {

                throw;
            }
            return View(_backlogVM);
        }

        [HttpPost]
        public ActionResult Index(string selectedFeatureID) {
            _currentProject = (ProjectVM)Session["CurrentProject"];
            _backlogVM.SelectedFeatureID = selectedFeatureID;
            _backlogVM.UserStories = new List<UserStory>();
            try {
                _backlogVM.Features = _featureManager.GetFeaturesByProjectID(_currentProject.ProjectID);
                _backlogVM.UserStories = _userStoryManager.GetFeatureUserStories(selectedFeatureID);
            } catch (Exception) {

                throw;
            }
            return View(_backlogVM);
        }

        // GET: Backlog/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Backlog/Create
        public ActionResult CreateFeature() {
            return View();
        }

        // POST: Backlog/Create
        [HttpPost]
        public ActionResult CreateFeature(Feature feature) {
            _currentProject = (ProjectVM)Session["CurrentProject"];
            try {
                // TODO: Add insert logic here
                _backlogVM.Features = _featureManager.GetFeaturesByProjectID(_currentProject.ProjectID);
                _featureManager.AddProjectFeature(new Feature() {
                    FeatureID = _currentProject.ProjectID + "." + (_backlogVM.Features.Count + 1),
                    ProjectID = _currentProject.ProjectID,
                    Name = feature.Name,
                    Description = feature.Description,
                    Priority = feature.Priority
                });
                return RedirectToAction("Index");
            }
            catch(Exception ex) {
                ViewBag.Error = ex.Message;
            }
            return View(feature);
        }

        public ActionResult CreateUserStory(BacklogVM backlog) {
            Session["FeatureID"] = backlog.SelectedFeatureID;
            return View();
        }

        [HttpPost]
        public ActionResult CreateUserStory(UserStory story) {
            string featureID = Session["FeatureID"].ToString();
            _currentProject = (ProjectVM)Session["CurrentProject"];
            try {
                List<UserStory> stories = _userStoryManager.GetFeatureUserStories(featureID);
                _userStoryManager.AddFeatureUserStory(new UserStory() {
                    StoryID = featureID + "." + (stories.Count + 1),
                    Person = story.Person,
                    FeatureID = featureID,
                    Action = story.Action,
                    Reason = story.Reason
                });
                return RedirectToAction("Index", "Backlog", featureID);
            } catch (Exception ex) {
                ViewBag.Error = ex.Message;
            }
            return View(story);
        }

        // GET: Backlog/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Backlog/Edit/5
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

        // GET: Backlog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Backlog/Delete/5
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
