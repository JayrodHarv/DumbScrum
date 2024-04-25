using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DumbScrumWebMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DumbScrumWebMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager userManager;

        // GET: Admin
        public ActionResult Index()
        {
            // return View(db.ApplicationUsers.ToList());
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return View(userManager.Users.OrderBy(n => n.DisplayName).ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = userManager.FindById(id);

            if (applicationUser == null) {
                return HttpNotFound();
            }

            var usrMgr = new LogicLayer.UserManager();
            var allRoles = usrMgr.GetAllRoles();

            var roles = userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;

            return View(applicationUser);
        }

        public ActionResult RemoveRole(string id, string role) {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(x => x.Id == id);

            if(role == "Admin") {
                var adminUsers = userManager.Users.ToList()
                    .Where(u => userManager.IsInRole(u.Id, role))
                    .ToList().Count();
                if (adminUsers < 2) {
                    ViewBag.Error = "Cannot remove the last administrator.";
                    return RedirectToAction("Details", "Admin", new { id = user.Id });
                }
            }
            userManager.RemoveFromRole(id, role);

            if(user.UserID != null) {
                try {
                    var usrMgr = new LogicLayer.UserManager();
                    usrMgr.RemoveUserRole((int)user.UserID, role);
                } catch (Exception) {

                }
            }
            return RedirectToAction("Details", "Admin", new { id = user.Id });
        }

        public ActionResult AddRole(string id, string role) {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(x => x.Id == id);

            userManager.AddToRole(id, role);

            if (user.UserID != null) {
                try {
                    var usrMgr = new LogicLayer.UserManager();
                    usrMgr.AddUserRole((int)user.UserID, role);
                } catch (Exception) {

                    throw;
                }
            }
            return RedirectToAction("Details", "Admin", new { id = user.Id });
        }
    }
}
