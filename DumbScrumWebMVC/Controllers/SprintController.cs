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
        MainManager _manager;

        public SprintController() {
            _manager = MainManager.GetMainManager();
        }

        [HttpGet]
        public ActionResult Index(string projectID) {
            SprintListVM sprintListVM = new SprintListVM();
            sprintListVM.ProjectID = projectID;
            try {
                sprintListVM.Sprints = _manager.SprintManager.GetSprintVMsByProjectID(projectID);
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return View(sprintListVM);
        }

        [HttpGet]
        public ActionResult Create(string projectID) {
            CreateSprintVM createSprintVM = new CreateSprintVM();
            createSprintVM.ProjectID = projectID;
            try {
                createSprintVM.Features = _manager.FeatureManager.GetFeaturesByProjectID(projectID);
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return View(createSprintVM);
        }

        [HttpPost]
        public ActionResult Create(CreateSprintVM createSprintVM) {
            try {
                SprintVM temp = _manager.SprintManager.GetSprintVMByFeatureID(createSprintVM.FeatureID);
                
                if (temp != null) {
                    TempData["Error"] = "Sprint already exists for the feature you selected.";
                    return View(createSprintVM);
                }

                if(!_manager.SprintManager.AddSprint(new Sprint() {
                    FeatureID = createSprintVM.FeatureID,
                    Name = createSprintVM.Name,
                    StartDate = createSprintVM.StartDate, 
                    EndDate = createSprintVM.EndDate
                })) {
                    TempData["Error"] = "Something went wrong while trying to plan this sprint.";
                    return View(createSprintVM);
                }

                List<UserStory> stories = _manager.UserStoryManager.GetFeatureUserStories(createSprintVM.FeatureID);
                Sprint sprint = _manager.SprintManager.GetSprintVMByFeatureID(createSprintVM.FeatureID);
                foreach (UserStory story in stories) {
                    Task task = new Task() {
                        SprintID = sprint.SprintID,
                        StoryID = story.StoryID,
                        Status = "To Do"
                    };
                    _manager.TaskManager.CreateTask(task);
                }
                return RedirectToAction("Index", "Sprint", new { projectID = createSprintVM.ProjectID });
            }
            catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return View(createSprintVM);
        }
    }
}
