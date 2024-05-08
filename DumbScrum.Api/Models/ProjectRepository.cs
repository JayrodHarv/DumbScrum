using DumbScrum.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DumbScrum.Api.Models {
    //public class ProjectRepository : IProjectRepository {
    //    private readonly AppDbContext db;

    //    public ProjectRepository(AppDbContext appDbContext) {
    //        db = appDbContext;
    //    }

    //    public async Task<IEnumerable<ProjectVM>> SelectAllProjects() {
    //        return await db.Database.Exe
    //    }

    //    public async Task<IEnumerable<ProjectVM>> SelectProjectsByUserID(int userId) {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<ProjectVM> SelectProjectVMByProjectID(string projectId) {
    //        return (ProjectVM)(from p in db.Projects
    //                           join u in db.Users
    //                           on p.UserId equals u.UserId
    //                           select new {
    //                               p.ProjectId, p.UserId, p.DateCreated, p.Status, p.Description, u.DisplayName
    //                           });

    //    }

    //    public async Task<bool> CreateProject(Project project) {
    //        var result = await db.Projects.AddAsync(project);
    //        return 0 < await db.SaveChangesAsync();
    //    }

    //    public async Task<bool> UpdateProject(Project project) {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<bool> DeleteProject(string projectId) {
    //        var result = await db.Projects.FirstOrDefaultAsync(p => p.ProjectId == projectId);
    //        if (result != null) {
    //            db.Projects.Remove(result);
    //            return 0 < await db.SaveChangesAsync();
    //        }
    //        return false;
    //    }
    //}
}
