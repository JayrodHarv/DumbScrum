using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Controllers
{
    public class SignInController : Controller
    {
        UserManager _userManager = new UserManager();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn(User user) {
            try {
                User loggedInUser = _userManager.SignInUser(user.Email, user.Password);
                Session.Add("LoggedInUser", loggedInUser);
                Session.Add("DisplayName", loggedInUser.DisplayName);
                // doesn't work because I'm redirecting to another controller and the ViewBag doesn't follow
                // ViewBag.DisplayName = loggedInUser.DisplayName;
                return RedirectToAction("Index", "Home");
            } catch (Exception ex) {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.InnerError = ex.InnerException;
                return View("SignInError");
            }
            //HttpContext.Response.Cookies.Add(new HttpCookie("LoggedInUser", user.UserID + user.Email + user.DisplayName));
        }

        public ActionResult SignOut() {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}