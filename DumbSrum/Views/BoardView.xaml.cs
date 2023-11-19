using DataObjects;
using DumbSrum.ToolWindows;
using LogicLayer;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace DumbSrum.Views {
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl {
        string projectID = string.Empty;
        SprintManager sprintManager = new SprintManager();

        public BoardView(string projectID) {
            this.projectID = projectID;
            DataContext = this;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            try {
                cbxSprint.ItemsSource = sprintManager.GetSprintsByProjectID(projectID);
            } catch (Exception) {
                MessageBox.Show("Error loading sprints");
            }
            cbxSprint.SelectedIndex = 0;
        }

        private void cbxSprint_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cbxSprint.SelectedItem != null) {
                Sprint sprint = cbxSprint.SelectedItem as Sprint;
                txtFeature.Text = sprint.FeatureID.ToString();
            }
        }

        private void btnPlanNewSprint_Click(object sender, RoutedEventArgs e) {
            try {
                var planNewSprintWindow = new PlanNewSprintWindow(projectID);
                var result = planNewSprintWindow.ShowDialog();
                if (result == true) {
                    MessageBox.Show("Sprint Successfully Planned.", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    // Automatically create a task for each user story of the sprint feature


                    cbxSprint.ItemsSource = sprintManager.GetSprintsByProjectID(projectID);
                } else {
                    MessageBox.Show("Sprint Not Planned", "Operation Aborted",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                    "Failed To Plan Sprint", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEditSprint_Click(object sender, RoutedEventArgs e) {

        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e) {

        }
    }
}
