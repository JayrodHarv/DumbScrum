using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public class MainManager {
        // Tyler Moment
        private static MainManager instance = null;
        public UserVM LoggedInUser { get; set; }

        public ProjectMemberVM ProjectMember { get; set; }

        private readonly Lazy<IUserManager> _userManager = new Lazy<IUserManager>(() => new UserManager());
        private readonly Lazy<IProjectManager> _projectManager = new Lazy<IProjectManager>(() => new ProjectManager());
        private readonly Lazy<IProjectMemberManager> _projectMemberManager = new Lazy<IProjectMemberManager>(() => new ProjectMemberManager());
        private readonly Lazy<IProjectRoleManager> _projectRoleManager = new Lazy<IProjectRoleManager>(() => new ProjectRoleManager());
        private readonly Lazy<IFeatureManager> _featureManager = new Lazy<IFeatureManager>(() => new FeatureManager());
        private readonly Lazy<IUserStoryManager> _userStoryManager = new Lazy<IUserStoryManager>(() => new UserStoryManager());
        private readonly Lazy<ITaskManager> _taskManager = new Lazy<ITaskManager>(() => new TaskManager());
        private readonly Lazy<ISprintManager> _sprintManager = new Lazy<ISprintManager>(() => new SprintManager());
        private readonly Lazy<IFeedMessageManager> _feedMessageManager = new Lazy<IFeedMessageManager>(() => new FeedMessageManager());
        private readonly Lazy<IFileManager> _fileManager = new Lazy<IFileManager>(() => new FileManager());
        
        public IUserManager UserManager => _userManager.Value;
        public IProjectManager ProjectManager => _projectManager.Value;
        public IProjectMemberManager ProjectMemberManager => _projectMemberManager.Value;
        public IProjectRoleManager ProjectRoleManager => _projectRoleManager.Value;
        public IFeatureManager FeatureManager => _featureManager.Value;
        public IUserStoryManager UserStoryManager => _userStoryManager.Value;
        public ITaskManager TaskManager => _taskManager.Value;
        public ISprintManager SprintManager => _sprintManager.Value;
        public IFeedMessageManager FeedMessageManager => _feedMessageManager.Value;
        public IFileManager FileManager => _fileManager.Value;

        public static MainManager GetMainManager() {
            return instance ?? (instance = new MainManager());
        }

        public void CleanManager() {
            instance = null;
            LoggedInUser = null;
        }
    }
}
