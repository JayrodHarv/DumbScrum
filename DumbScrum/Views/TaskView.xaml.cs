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
        FeedMessageManager feedMessageManager = new FeedMessageManager();
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
                }
                tbUserStory.Text = task.Story;
                tbStatus.Text = task.Status;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            switch(task.Status) {
                case "In Progress":
                    btnSendToBeReviewed.Visibility = Visibility.Visible;
                    tabReview.Visibility = Visibility.Collapsed;
                    break;
                case "Needs Reviewed":
                    btnSendToBeReviewed.Visibility = Visibility.Collapsed;
                    tabReview.Visibility = Visibility.Visible;
                    break;
                case "Complete":
                    btnSendToBeReviewed.Visibility = Visibility.Collapsed;
                    tabReview.Visibility = Visibility.Collapsed;
                    break;
            }

            tabUseCase.Content = new FileUploadView(projectID, taskID, "Use Case");
            tabStoredProcedure.Content = new FileUploadView(projectID, taskID, "Stored Procedure Specification");
            tabInterfaces.Content = new FileUploadView(projectID, taskID, "User Interface");
            tabERDiagram.Content = new FileUploadView(projectID, taskID, "ER Diagram");
            tabDD.Content = new FileUploadView(projectID, taskID, "Data Dictionary");
            tabDM.Content = new FileUploadView(projectID, taskID, "Data Model");
        }

        private void btnSendToBeReviewed_Click(object sender, RoutedEventArgs e) {
            try {
                if (taskManager.UpdateTaskStatus(taskID, "Needs Reviewed")) {
                    MainWindow window = (MainWindow)Window.GetWindow(this);
                    ProjectView projectView = (ProjectView)window.CurrentView;
                    projectView.CurrentProjectView = new BoardView(projectID, user);
                    MessageBox.Show("Success!");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSubmitReview_Click(object sender, RoutedEventArgs e) {
            if(user.UserID == task.UserID) {
                MessageBox.Show("You can't review your own work, it defeats the purpose of reviewing things.");
                return;
            }
            if (tbReviewNotes.Text == "") {
                MessageBox.Show("Please provide at least some message for the review.");
                return;
            }
            if (cbUseCase.IsChecked == true &&
                cbSPS.IsChecked == true &&
                cbUserInterface.IsChecked == true &&
                cbERD.IsChecked == true &&
                cbDD.IsChecked == true &&
                cbDM.IsChecked == true) {
                // all of the work looked good to the reviewer | change task status to complete
                if (taskManager.UpdateTaskStatus(taskID, "Complete")) {
                    SumbitReview();
                }
            } else {
                if (taskManager.UpdateTaskStatus(taskID, "In Progress")) {
                    SumbitReview();
                }
            }
        }

        private void SumbitReview() {
            string messageText = "Review of task " + taskID + ":\n" + tbReviewNotes.Text;
            FeedMessage message = new FeedMessage() {
                SprintID = task.SprintID,
                UserID = user.UserID,
                Text = messageText,
                SentAt = DateTime.Now
            };
            feedMessageManager.CreateFeedMessage(message);
            MainWindow window = (MainWindow)Window.GetWindow(this);
            ProjectView projectView = (ProjectView)window.CurrentView;
            projectView.CurrentProjectView = new BoardView(projectID, user);
            MessageBox.Show("Review successfully submited. Feed message created showing the review message.");
        }
    }
}
