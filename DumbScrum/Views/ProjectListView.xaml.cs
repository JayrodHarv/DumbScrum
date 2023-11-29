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
        ProjectManager projectManager = new ProjectManager();
        int userID;
        public ProjectListView(int userID) {
            this.userID = userID;
            InitializeComponent();
        }

        private void btnJoinProject_Click(object sender, RoutedEventArgs e) {
            if (lvProjects.SelectedItem == null) {
                MessageBox.Show("Please select a project to join it.");
                return;
            }
            ProjectVM project = (ProjectVM)lvProjects.SelectedItem;
            if(userID == project.UserID) {
                MessageBox.Show("You automatically join your own projects. No need to join again.");
                return;
            }
            List<ProjectVM> projects = projectManager.GetProjectsByUserID(userID);
            if(projects.Contains(project)) {
                MessageBox.Show("You have already joined this project");
                return;
            }
            try {
                if(projectManager.JoinProject(project.ProjectID, userID)) {
                    MessageBox.Show("Sucessfully joined " + project.ProjectID + "!");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            lvProjects.ItemsSource = projectManager.GetAllProjects();
        }
    }
}
