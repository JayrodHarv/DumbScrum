using DataObjects;
using DumbScrum.ToolWindows;
using DumbScrum.UserControls;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DumbScrum.Views {
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl {
        string projectID = string.Empty;
        SprintManager sprintManager = new SprintManager();
        TaskManager taskManager = new TaskManager();
        List<SprintVM> sprints = new List<SprintVM>();

        public BoardView(string projectID) {
            this.projectID = projectID;
            DataContext = this;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            try {
                sprints = sprintManager.GetSprintVMsByProjectID(projectID);
                cbxSprint.ItemsSource = sprints;
            } catch (Exception) {
                MessageBox.Show("Error loading sprints");
            }
            // select the current sprint by checking if the current date is in-between the start and end date of a sprint
            DateTime now = DateTime.Now;
            foreach (SprintVM s in sprints) {
                if(now > s.StartDate && now <= s.EndDate) {
                    cbxSprint.SelectedItem = s;
                }
            }
        }

        private void cbxSprint_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cbxSprint.SelectedItem != null) {
                SprintVM sprint = cbxSprint.SelectedItem as SprintVM;
                txtFeature.Text = sprint.FeatureName;
                txtDateRange.Text = sprint.StartDate.ToShortDateString() + " - " + sprint.EndDate.ToShortDateString();
                try {
                    icToDoTasks.ItemsSource = taskManager.GetSprintTaskVMsByStatus(sprint.SprintID, "To Do");
                    icInProgressTasks.ItemsSource = taskManager.GetSprintTaskVMsByStatus(sprint.SprintID, "In Progress");
                    icNeedsReviewedTasks.ItemsSource = taskManager.GetSprintTaskVMsByStatus(sprint.SprintID, "Needs Reviewed");
                    icCompleteTasks.ItemsSource = taskManager.GetSprintTaskVMsByStatus(sprint.SprintID, "Complete");
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e) {

        }

        private void SrumBoardItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            ProjectView projectView = (ProjectView)window.CurrentView;
            SrumBoardItem item = sender as SrumBoardItem;
            int taskID = int.Parse(item.lblTaskID.Content.ToString());
            projectView.CurrentProjectView = new TaskView(projectID, taskID);
        }
    }
}
