using DataObjects;
using LogicLayer;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DumbScrum.Views {
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : UserControl {
        string projectID;
        int taskID;
        TaskVM task;
        UserVM user;
        TaskManager taskManager = new TaskManager();
        UserManager userManager = new UserManager();
        public TaskView(string projectID, int taskID, UserVM user) {
            this.projectID = projectID;
            this.taskID = taskID;
            this.user = user;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            try {
                task = taskManager.GetTask(taskID);
                tbTaskID.Text = task.TaskID.ToString();
                if(task.UserID != 0) { // it's set to 0 if userid is null in the accessor
                    User user = userManager.GetUser(task.UserID);
                    tbUser.Text = user.DisplayName;
                    btnCommitToTask.Visibility = Visibility.Collapsed;
                }
                tbUserStory.Text = task.Story;
                tbStatus.Text = task.Status;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            switch(task.Status) {
                case "In Progress":
                    btnSendToBeReviewed.Visibility = Visibility.Visible;
                    break;
                case "Needs Reviewed":
                    break;
            }

            tabUseCase.Content = new FileUploadView(projectID, taskID, "Use Case");
            tabStoredProcedure.Content = new FileUploadView(projectID, taskID, "Stored Procedure Specification");
            tabInterfaces.Content = new FileUploadView(projectID, taskID, "User Interface");
            tabERDiagram.Content = new FileUploadView(projectID, taskID, "ER Diagram");
            tabDD.Content = new FileUploadView(projectID, taskID, "Data Dictionary");
            tabDM.Content = new FileUploadView(projectID, taskID, "Data Model");
        }

        private void btnCommitToTask_Click(object sender, RoutedEventArgs e) {

        }

        private void btnSendToBeReviewed_Click(object sender, RoutedEventArgs e) {
            try {
                if (taskManager.UpdateTaskStatus(taskID, "Needs Reviewed")) {
                    MessageBox.Show("Success!");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
