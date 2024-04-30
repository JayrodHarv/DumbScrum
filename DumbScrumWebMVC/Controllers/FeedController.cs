using DataObjects;
using DumbScrumWebMVC.Models;
using LogicLayer;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Controllers {
    public class FeedController : Controller {
        MainManager _manager;

        public FeedController() {
            _manager = MainManager.GetMainManager();
        }

        [HttpGet]
        public ActionResult Index(string projectID) {
            FeedListVM feedListVM = new FeedListVM();
            feedListVM.ProjectID = projectID;
            feedListVM.FeedMessages = new List<FeedMessageVM>();
            try {
                feedListVM.Sprints = _manager.SprintManager.GetSprintVMsByProjectID(projectID);
                if(feedListVM.Sprints.Count == 0) {
                    ViewBag.FeedError = "This project has no sprints";
                    return View(feedListVM);
                }
                List<SelectListItem> sprints = new List<SelectListItem>();
                foreach (SprintVM sprint in feedListVM.Sprints) {
                    sprints.Add(new SelectListItem { Text = sprint.Name, Value = sprint.SprintID.ToString() });
                }
                feedListVM.SprintDropdownItems = sprints;
                feedListVM.CurrentSprint = feedListVM.Sprints.Find(sprint => sprint.StartDate >= DateTime.Now && sprint.EndDate <= DateTime.Now);
                if(feedListVM.CurrentSprint != null) {
                    feedListVM.FeedMessages = _manager.FeedMessageManager.GetSprintFeedMessages(feedListVM.CurrentSprint.SprintID);
                } else {
                    List<SprintVM> temp = feedListVM.Sprints.OrderBy(sprint => sprint.StartDate).ToList();
                    feedListVM.FeedMessages = _manager.FeedMessageManager.GetSprintFeedMessages(temp[0].SprintID);
                }
            } catch (Exception ex) {
                TempData["Error"] = ex.Message;
            }
            return View(feedListVM);
        }

        [HttpPost]
        public ActionResult Index(string projectID, string sprintFilter, string feedMessageInput) {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(User.Identity.GetUserId());
            FeedListVM feedListVM = new FeedListVM();
            feedListVM.ProjectID = projectID;
            feedListVM.FeedMessages = new List<FeedMessageVM>();
            try {
                feedListVM.Sprints = _manager.SprintManager.GetSprintVMsByProjectID(projectID);
                if (feedListVM.Sprints.Count == 0) {
                    ViewBag.FeedError = "This project has no sprints";
                    return View(feedListVM);
                }
                List<SelectListItem> sprints = new List<SelectListItem>();
                foreach (SprintVM sprint in feedListVM.Sprints) {
                    sprints.Add(new SelectListItem { Text = sprint.Name, Value = sprint.SprintID.ToString(), Selected = sprint.Name == sprintFilter });
                }
                feedListVM.SprintDropdownItems = sprints;
                feedListVM.CurrentSprint = feedListVM.Sprints.Find(sprint => sprint.SprintID == Convert.ToInt32(sprintFilter));
                if (feedListVM.CurrentSprint != null) {
                    if(feedMessageInput != null && feedMessageInput != "") {
                        _manager.FeedMessageManager.CreateFeedMessage(new FeedMessage() {
                            SprintID = feedListVM.CurrentSprint.SprintID,
                            UserID = (int)user.UserID,
                            Text = feedMessageInput,
                            SentAt = DateTime.Now,
                        });
                    }
                    feedListVM.FeedMessages = _manager.FeedMessageManager.GetSprintFeedMessages(feedListVM.CurrentSprint.SprintID);
                } else {
                    List<SprintVM> temp = feedListVM.Sprints.OrderBy(sprint => sprint.StartDate).ToList();
                    feedListVM.FeedMessages = _manager.FeedMessageManager.GetSprintFeedMessages(temp[0].SprintID);
                }
            } catch (Exception) {

                throw;
            }
            return View(feedListVM);
        }
    }
}
