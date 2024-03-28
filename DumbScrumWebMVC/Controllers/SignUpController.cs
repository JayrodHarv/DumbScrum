using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DumbScrumWebMVC.Controllers
{
    public class SignUpController : Controller
    {
        UserManager _userManager = new UserManager();
        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp(User user) {
            try {
                string path = @"C:\Dumb Scrum\DumbScrum 03-18\DumbScrum\DumbScrumWebMVC\Images\Sample_User_Icon.png";
                user.Pfp = _userManager.GetFileInBinary(path);
                _userManager.SignUpUser(user);
            } catch (Exception ex) {
                // we need to go to an error page
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.InnerError = ex.InnerException;
                return View("SignUpError");
            }
            return RedirectToAction("Index", "SignIn");
        }
    }
}