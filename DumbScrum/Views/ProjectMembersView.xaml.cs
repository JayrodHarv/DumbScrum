using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DumbScrum.Views {
    /// <summary>
    /// Interaction logic for ProjectMembersView.xaml
    /// </summary>
    public partial class ProjectMembersView : UserControl {
        UserManager userManager = new UserManager();
        TaskManager taskManager = new TaskManager();
        ProjectManager projectManager = new ProjectManager();
        string projectID;
        public ProjectMembersView(string projectID) {
            this.projectID = projectID;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            RefreshMemberList();
        }

        private void RefreshMemberList() {
            List<UserVM> members = userManager.GetProjectMembers(projectID);
            foreach (UserVM member in members) {
                List<TaskVM> tasks = taskManager.GetTaskVMsByUserID(member.UserID);
                member.InProgressTasksCount = tasks.FindAll(t => t.Status == "In Progress").Count();
                member.InReviewTasksCount = tasks.FindAll(t => t.Status == "Needs Reviewed").Count();
                member.CompletedTasksCount = tasks.FindAll(t => t.Status == "Complete").Count();
            }
            lvMembers.ItemsSource = members;
        }

        private void btnKickMember_Click(object sender, RoutedEventArgs e) {
            if (lvMembers.SelectedItem == null) {
                MessageBox.Show("You must select the project memeber you would like to kick from the project.");
                return;
            }
            UserVM user = (UserVM)lvMembers.SelectedItem;
            // check if user is project owner
            ProjectVM project = projectManager.GetProjectVMByProjectID(projectID);
            if(user.UserID == project.UserID) {
                MessageBox.Show("You can't kick yourself from your own project");
                return;
            }
            var result = MessageBox.Show("Are you sure that you want to kick " + user.DisplayName + " from this project?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes) {
                try {
                    if (projectManager.LeaveProject(user.UserID, projectID)) {
                        RefreshMemberList();
                        MessageBox.Show("Member Kicked From Project.");
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
