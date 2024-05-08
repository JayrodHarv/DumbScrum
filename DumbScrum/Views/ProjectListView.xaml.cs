using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DumbScrum.Views {
    /// <summary>
    /// Interaction logic for ProjectsView.xaml
    /// </summary>
    public partial class ProjectListView : UserControl {
        MainManager _manager;
        public ProjectListView() {
            _manager = MainManager.GetMainManager();
            InitializeComponent();
        }

        private static ProjectListView _projectListView = null;

        public static ProjectListView GetViewHousekeepingSchedulePage() {
            return _projectListView ?? (_projectListView = new ProjectListView());
        }

        //private void btnJoinProject_Click(object sender, RoutedEventArgs e) {
        //    if (lvProjects.SelectedItem == null) {
        //        MessageBox.Show("Please select a project to join it.");
        //        return;
        //    }
        //    ProjectVM project = (ProjectVM)lvProjects.SelectedItem;
        //    if(userID == project.UserID) {
        //        MessageBox.Show("You automatically join your own projects. No need to join again.");
        //        return;
        //    }
        //    List<ProjectVM> projects = _manager.ProjectManager.GetProjectsByUserID(userID);
        //    if(projects.Contains(project)) {
        //        MessageBox.Show("You have already joined this project");
        //        return;
        //    }
        //    try {
        //        if(_manager.ProjectMemberManager.AddProjectMember(project.ProjectID, userID)) {
        //            MessageBox.Show("Sucessfully joined " + project.ProjectID + "!");
        //        }
        //    } catch (Exception ex) {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            lvProjects.ItemsSource = _manager.ProjectManager.GetAllProjects();
        }
    }
}
