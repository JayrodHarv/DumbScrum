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
        MainManager _manager;
        string projectID;
        public ProjectMembersView(string projectID) {
            _manager = MainManager.GetMainManager();
            this.projectID = projectID;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            RefreshMemberList();
        }

        private void RefreshMemberList() {
            List<ProjectMemberListVM> members = _manager.ProjectMemberManager.GetProjectMembers(projectID);
            lvMembers.ItemsSource = members;
        }

        private void btnKickMember_Click(object sender, RoutedEventArgs e) {
            if (lvMembers.SelectedItem == null) {
                MessageBox.Show("You must select the project memeber you would like to kick from the project.");
                return;
            }
            UserVM user = (UserVM)lvMembers.SelectedItem;
            // check if user is project owner
            ProjectVM project = _manager.ProjectManager.GetProjectVMByProjectID(projectID);
            if(user.UserID == project.UserID) {
                MessageBox.Show("You can't kick yourself from your own project");
                return;
            }
            var result = MessageBox.Show("Are you sure that you want to kick " + user.DisplayName + " from this project?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes) {
                try {
                    if (_manager.ProjectMemberManager.MemberLeaveProject(user.UserID, projectID)) {
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
