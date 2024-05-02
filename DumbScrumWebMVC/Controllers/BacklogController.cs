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
        MainManager _manager;

        public BacklogController() {
            _manager = MainManager.GetMainManager();
        }

        [HttpGet]
        public ActionResult Index(string projectID) {
            ViewBag.Tab = "Backlog";
            BacklogVM backlogVM = new BacklogVM();
            backlogVM.ProjectID = projectID;
            backlogVM.UserStories = new List<UserStory>();
            try {
                backlogVM.Features = _manager.FeatureManager.GetFeaturesByProjectID(projectID);
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return View(backlogVM);
        }

        [HttpPost]
        public ActionResult Index(string projectID, string selectedFeatureID) {
            ViewBag.Tab = "Backlog";
            BacklogVM backlogVM = new BacklogVM();
            backlogVM.ProjectID = projectID;
            backlogVM.SelectedFeatureID = selectedFeatureID;
            backlogVM.UserStories = new List<UserStory>();
            try {
                backlogVM.Features = _manager.FeatureManager.GetFeaturesByProjectID(projectID);
                backlogVM.UserStories = _manager.UserStoryManager.GetFeatureUserStories(selectedFeatureID);
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return View(backlogVM);
        }

        [HttpGet]
        public ActionResult CreateFeature(string projectID) {
            Feature feature = new Feature();
            feature.ProjectID = projectID;
            return View(feature);
        }

        [HttpPost]
        public ActionResult CreateFeature(Feature feature) {
            BacklogVM backlogVM = new BacklogVM();
            try {
                backlogVM.Features = _manager.FeatureManager.GetFeaturesByProjectID(feature.ProjectID);

                Feature newFeature = new Feature() {
                    FeatureID = feature.ProjectID + "." + (backlogVM.Features.Count + 1),
                    ProjectID = feature.ProjectID,
                    Name = feature.Name,
                    Description = feature.Description,
                    Priority = feature.Priority
                };

                if(_manager.FeatureManager.AddProjectFeature(newFeature)) {
                    TempData["Success"] = "Successfully created feature: " + feature.Name;
                    return RedirectToAction("Index", "Backlog", new { projectID = feature.ProjectID });
                } else {
                    TempData["Warning"] = "Something went wrong while creating feature: " + feature.Name;
                }
            }
            catch(Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return View(feature);
        }

        [HttpGet]
        public ActionResult CreateUserStory(string projectID, string featureID) {
            CreateUserStoryVM createStoryVM = new CreateUserStoryVM();
            createStoryVM.ProjectID = projectID;
            createStoryVM.FeatureID = featureID;
            return View(createStoryVM);
        }

        [HttpPost]
        public ActionResult CreateUserStory(CreateUserStoryVM createStoryVM) {
            try {
                List<UserStory> stories = _manager.UserStoryManager.GetFeatureUserStories(createStoryVM.FeatureID);
                _manager.UserStoryManager.AddFeatureUserStory(new UserStory() {
                    StoryID = createStoryVM.FeatureID + "." + (stories.Count + 1),
                    Person = createStoryVM.Person,
                    FeatureID = createStoryVM.FeatureID,
                    Action = createStoryVM.Action,
                    Reason = createStoryVM.Reason
                });
                return RedirectToAction("Index", "Backlog", new { projectID = createStoryVM.ProjectID });
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return View(createStoryVM);
        }
    }
}
