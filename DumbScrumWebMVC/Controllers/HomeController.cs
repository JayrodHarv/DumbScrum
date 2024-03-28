using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            if (Session["LoggedInUser"] != null) {
                UserVM loggedInUser = (UserVM)Session["LoggedInUser"];
                ViewBag.DisplayName = loggedInUser.DisplayName;
            }
            return View();
        }
    }
}