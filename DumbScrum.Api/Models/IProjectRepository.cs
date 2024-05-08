using DumbScrum.Models;

namespace DumbScrum.Api.Models {
    public interface IProjectRepository {
        Task<ProjectVM> SelectProjectVMByProjectID(string projectId);
        Task<IEnumerable<ProjectVM>> SelectProjectsByUserID(int userId);
        Task<IEnumerable<ProjectVM>> SelectAllProjects();
        Task<bool> CreateProject(Project project);
        Task<bool> UpdateProject(Project project);
        Task<bool> DeleteProject(string projectId);
    }
}
