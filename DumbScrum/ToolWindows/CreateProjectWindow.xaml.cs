using DataObjects;
using LogicLayer;
using System;
using System.Windows;

namespace DumbScrum {
    /// <summary>
    /// Interaction logic for CreateProjectWindow.xaml
    /// </summary>
    public partial class CreateProjectWindow : Window {
        User user;
        public CreateProjectWindow(User user) {
            InitializeComponent();
            this.user = user;
        }

        private void btnCreateProject_Click(object sender, RoutedEventArgs e) {
            if(txtProjectTitle.Text == "") {
                MessageBox.Show("You have to give your project a name.");
                return;
            }
            if(txtProjectOwner.Text == "") {
                MessageBox.Show("You have to have a project owner.");
                return;
            }
            if(txtDescription.Text == "") {
                MessageBox.Show("Your project must have a description.");
            }

            ProjectManager projectManager = new ProjectManager();

            try {
                if (projectManager.AddProject(txtProjectTitle.Text, GoogleDriveHelper.CreateProjectDriveFolder(txtProjectTitle.Text), txtProjectOwner.Text, txtDescription.Text, user.UserID)) {
                    this.DialogResult = true;
                } else {
                    MessageBox.Show("Failed to create project.");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }
    }
}
