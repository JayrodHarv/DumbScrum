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
            ViewBag.Tab = "Sprints";
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
            createSprintVM.StartDate = DateTime.Now.Date;
            createSprintVM.EndDate = DateTime.Now.Date;
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
                if (_manager.SprintManager.GetSprintVMByFeatureID(createSprintVM.FeatureID) == null) {
                    Sprint sprint = new Sprint() {
                        FeatureID = createSprintVM.FeatureID,
                        Name = createSprintVM.Name,
                        StartDate = createSprintVM.StartDate,
                        EndDate = createSprintVM.EndDate
                    };

                    if (_manager.SprintManager.AddSprint(sprint)) {
                        TempData["Success"] = "Successfully planned sprint: " + sprint.Name;
                        return RedirectToAction("Index", "Sprint", new { projectID = createSprintVM.ProjectID });
                    } else {
                        TempData["Warning"] = "Something went wrong while trying to plan this sprint.";
                    }
                } else {
                    TempData["Warning"] = "Sprint already exists for the feature you selected.";
                }
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }

            try {
                createSprintVM.Features = _manager.FeatureManager.GetFeaturesByProjectID(createSprintVM.ProjectID);
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return View(createSprintVM);
        }
    }
}
