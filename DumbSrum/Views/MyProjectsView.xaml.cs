using DataObjects;
using LogicLayer;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DumbSrum.Views {
    /// <summary>
    /// Interaction logic for MyProjectsView.xaml
    /// </summary>
    public partial class MyProjectsView : UserControl {
        ProjectManager projectManager = new ProjectManager();
        User user;
        public MyProjectsView(User user) {
            InitializeComponent();
            this.user = user;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            lvProjects.ItemsSource = projectManager.GetProjectsByUserID(user.UserID);
        }

        private void btnOpenProject_Click(object sender, RoutedEventArgs e) {
            if(lvProjects.SelectedItem == null) {
                MessageBox.Show("Please select a project to open it.");
                return;
            }
            MainWindow parentWindow = (MainWindow) Window.GetWindow(this);

            Project selectedProject = (Project)lvProjects.SelectedItem;

            parentWindow.CurrentView = new ProjectView(selectedProject.ProjectID);
        }

        private void btnJoinProject_Click(object sender, RoutedEventArgs e) {

        }

        private void btnCreateProject_Click(object sender, RoutedEventArgs e) {
            try {
                var createProjectWindow = new CreateProjectWindow(user);
                var result = createProjectWindow.ShowDialog();
                if (result == true) {
                    MessageBox.Show("Project Successfully Created.", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    lvProjects.ItemsSource = projectManager.GetProjectsByUserID(user.UserID);
                } else {
                    MessageBox.Show("Project Not Created", "Operation Aborted",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                    "Failed To Create Project", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLeaveProject_Click(object sender, RoutedEventArgs e) {

        }
    }
}
