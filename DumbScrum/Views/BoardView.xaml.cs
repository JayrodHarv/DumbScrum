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
        UserVM user;
        SprintManager sprintManager = new SprintManager();
        TaskManager taskManager = new TaskManager();
        List<SprintVM> sprints = new List<SprintVM>();

        public BoardView(string projectID, UserVM user) {
            this.projectID = projectID;
            this.user = user;
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
                    RefreshBoard(sprint.SprintID);
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e) {

        }

        public void RefreshBoard(int sprintID) {
            icToDoTasks.Items.Clear();
            icInProgressTasks.Items.Clear();
            icNeedsReviewedTasks.Items.Clear();
            icCompleteTasks.Items.Clear();
            List<TaskVM> tasks = taskManager.GetSprintTaskVMs(sprintID);
            foreach (TaskVM task in tasks) {
                switch (task.Status) {
                    case "To Do":
                        icToDoTasks.Items.Add(new SrumBoardItem(task, user, projectID));
                        break;
                    case "In Progress":
                        icInProgressTasks.Items.Add(new SrumBoardItem(task, user, projectID));
                        break;
                    case "Needs Reviewed":
                        icNeedsReviewedTasks.Items.Add(new SrumBoardItem(task, user, projectID));
                        break;
                    case "Complete":
                        icCompleteTasks.Items.Add(new SrumBoardItem(task, user, projectID));
                        break;
                }
            }
        }
    }
}
