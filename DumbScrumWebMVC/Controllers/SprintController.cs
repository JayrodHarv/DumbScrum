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

        [HttpGet]
        public ActionResult Edit(string projectID, int sprintID) {
            EditSprintVM editSprintVM = new EditSprintVM();
            editSprintVM.ProjectID = projectID;
            try {
                SprintVM sprintVM = _manager.SprintManager.GetSprintVMBySprintID(sprintID);
                Session["OldSprint"] = sprintVM;
                if (sprintVM != null) {
                    editSprintVM.SprintID = sprintVM.SprintID;
                    editSprintVM.FeatureID = sprintVM.FeatureID;
                    editSprintVM.Name = sprintVM.Name;
                    editSprintVM.StartDate = sprintVM.StartDate;
                    editSprintVM.EndDate = sprintVM.EndDate;
                } else {
                    TempData["Warning"] = "Couldn't retreive data for sprint";
                    return RedirectToAction("Index", new { projectID });
                }
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return View(editSprintVM);
        }

        [HttpPost]
        public ActionResult Edit(EditSprintVM editSprintVM) {
            // TODO: Add ability to change sprint feature
            Sprint sprintFromSession = (SprintVM)Session["OldSprint"];
            try {
                Sprint newSprint = new Sprint() {
                    FeatureID = editSprintVM.FeatureID,
                    Name = editSprintVM.Name,
                    StartDate = editSprintVM.StartDate,
                    EndDate = editSprintVM.EndDate
                };

                Sprint oldSprint = new Sprint() {
                    SprintID = sprintFromSession.SprintID,
                    FeatureID = sprintFromSession.FeatureID,
                    Name = sprintFromSession.Name,
                    StartDate = sprintFromSession.StartDate,
                    EndDate = sprintFromSession.EndDate
                };

                if (_manager.SprintManager.EditSprint(newSprint, oldSprint)) {
                    TempData["Success"] = "Successfully edited sprint: " + newSprint.Name;
                    return RedirectToAction("Index", "Sprint", new { projectID = editSprintVM.ProjectID });
                } else {
                    TempData["Warning"] = "Something went wrong while trying to plan this sprint.";
                }
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }

            try {
                if (sprintFromSession != null) {
                    editSprintVM.SprintID = sprintFromSession.SprintID;
                    editSprintVM.FeatureID = sprintFromSession.FeatureID;
                    editSprintVM.Name = sprintFromSession.Name;
                    editSprintVM.StartDate = sprintFromSession.StartDate;
                    editSprintVM.EndDate = sprintFromSession.EndDate;
                } else {
                    TempData["Warning"] = "Couldn't retreive data for sprint";
                    return RedirectToAction("Index", new { projectID = editSprintVM.ProjectID });
                }
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return View(editSprintVM);
        }

        [HttpPost]
        public ActionResult Delete(string projectID, int sprintID) {
            try {
                if(_manager.SprintManager.CancelSprint(sprintID)) {
                    TempData["Success"] = "Successfully cancelled sprint";
                } else {
                    TempData["Warning"] = "Something went wrong while cancelling sprint";
                }
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index", "Sprint", new { projectID });
        }
    }
}
