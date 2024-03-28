using DataObjects;
using DumbScrumWebMVC.Models;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Controllers
{
    public class FeedController : Controller
    {
        FeedMessageManager _feedMessageManager = new FeedMessageManager();
        SprintManager _sprintManager = new SprintManager();
        ProjectVM _currentProject;
        // GET: Feed
        public ActionResult Index() {
            _currentProject = (ProjectVM)Session["CurrentProject"];
            FeedListVM feedListVM = new FeedListVM();
            feedListVM.FeedMessages = new List<FeedMessageVM>();
            try {
                feedListVM.Sprints = _sprintManager.GetSprintVMsByProjectID(_currentProject.ProjectID);
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
                    feedListVM.FeedMessages = _feedMessageManager.GetSprintFeedMessages(feedListVM.CurrentSprint.SprintID);
                } else {
                    List<SprintVM> temp = feedListVM.Sprints.OrderBy(sprint => sprint.StartDate).ToList();
                    feedListVM.FeedMessages = _feedMessageManager.GetSprintFeedMessages(temp[0].SprintID);
                }
            } catch (Exception) {

                throw;
            }
            return View(feedListVM);
        }

        [HttpPost]
        public ActionResult Index(string sprintFilter, string feedMessageInput) {
            _currentProject = (ProjectVM)Session["CurrentProject"];
            UserVM user = Session["LoggedInUser"] as UserVM;
            FeedListVM feedListVM = new FeedListVM();
            feedListVM.FeedMessages = new List<FeedMessageVM>();
            try {
                feedListVM.Sprints = _sprintManager.GetSprintVMsByProjectID(_currentProject.ProjectID);
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
                        _feedMessageManager.CreateFeedMessage(new FeedMessage() {
                            SprintID = feedListVM.CurrentSprint.SprintID,
                            UserID = user.UserID,
                            Text = feedMessageInput,
                            SentAt = DateTime.Now,
                        });
                    }
                    feedListVM.FeedMessages = _feedMessageManager.GetSprintFeedMessages(feedListVM.CurrentSprint.SprintID);
                } else {
                    List<SprintVM> temp = feedListVM.Sprints.OrderBy(sprint => sprint.StartDate).ToList();
                    feedListVM.FeedMessages = _feedMessageManager.GetSprintFeedMessages(temp[0].SprintID);
                }
            } catch (Exception) {

                throw;
            }
            return View(feedListVM);
        }

        // POST: Feed/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Feed/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Feed/Edit/5
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

        // GET: Feed/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Feed/Delete/5
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
