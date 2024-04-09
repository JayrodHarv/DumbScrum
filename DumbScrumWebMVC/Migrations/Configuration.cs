namespace DumbScrumWebMVC.Migrations
{
    using DumbScrumWebMVC.Models;
    using LogicLayer;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DumbScrumWebMVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DumbScrumWebMVC.Models.ApplicationDbContext";
        }

        protected override void Seed(DumbScrumWebMVC.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            const string admin = "admin@gmail.com";
            const string adminPassword = "P@ssw0rd";

            LogicLayer.UserManager userMgr = new LogicLayer.UserManager();
            // will add this later
            // var roles = userMgr.GetUserRoles();
            // but for now
            List<string> roles = new List<string>() {
                "Admin", "User"
            };

            foreach(var role in roles) {
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = role });
            }

            if(!roles.Contains("Admin")) {
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Admin" });
            }

            // if admin user doesn't already exist
            if(!context.Users.Any(u => u.UserName == admin)) {
                // Create new admin user
                var user = new ApplicationUser() {
                    UserName = admin,
                    Email = admin,
                    GivenName = "Admin",
                    FamilyName = "Joe"
                };

                // Add the newly created admin user
                IdentityResult result = userManager.Create(user, adminPassword);
                context.SaveChanges(); // Updates the database

                if(result.Succeeded) {
                    userManager.AddToRole(user.Id, "Admin");
                    context.SaveChanges();
                }
            }
        }
    }
}
