using DumbScrum.Models;
using Microsoft.EntityFrameworkCore;

namespace DumbScrum.Api.Models {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<DumbScrum.Models.Task> Tasks { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
        public DbSet<FeedMessage> FeedMessages { get; set; }
        public DbSet<DumbScrum.Models.File> Files { get; set; }


        //public DbSet<User> Users { get; set; }
        //public DbSet<Project> Projects { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder) {
        //    base.OnModelCreating(modelBuilder);

        //    // Seed User Table
        //    modelBuilder.Entity<User>().HasData(
        //        new User {
        //            UserID = 1,
        //            Email = "admin@gmail.com",
        //            DisplayName = "Admin",

        //        }
        //    );
        //}
    }
}
