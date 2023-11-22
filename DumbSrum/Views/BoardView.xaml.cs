using DataObjects;
using DumbSrum.ToolWindows;
using DumbSrum.UserControls;
using LogicLayer;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DumbSrum.Views {
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl {
        string projectID = string.Empty;
        SprintManager sprintManager = new SprintManager();
        TaskManager taskManager = new TaskManager();

        public BoardView(string projectID) {
            this.projectID = projectID;
            DataContext = this;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            try {
                cbxSprint.ItemsSource = sprintManager.GetSprintVMsByProjectID(projectID);
            } catch (Exception) {
                MessageBox.Show("Error loading sprints");
            }
            cbxSprint.SelectedIndex = 0;
        }

        private void cbxSprint_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cbxSprint.SelectedItem != null) {
                SprintVM sprint = cbxSprint.SelectedItem as SprintVM;
                txtFeature.Text = sprint.FeatureName;
                try {
                    icToDoTasks.ItemsSource = taskManager.GetTaskVMsBySprintID(sprint.SprintID);
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
            projectView.CurrentProjectView = new TaskView(taskID);
        }
    }
}
