using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DumbSrum.ToolWindows {
    /// <summary>
    /// Interaction logic for PlanNewSprint.xaml
    /// </summary>
    public partial class PlanNewSprintWindow : Window {
        string projectID = string.Empty;
        FeatureManager featureManager = new FeatureManager();
        UserStoryManager userStoryManager = new UserStoryManager();
        SprintManager sprintManager = new SprintManager();
        public PlanNewSprintWindow(string projectID) {
            this.projectID = projectID;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            List<FeatureVM> features = featureManager.GetFeaturesByProjectID(projectID);
            cboFeature.ItemsSource = features;
            dpStartDate.SelectedDate = DateTime.Now;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }

        private void btnPlanSprint_Click(object sender, RoutedEventArgs e) {
            if (cboFeature.SelectedItem == null) {
                MessageBox.Show("Please select a feature to add to the sprint.");
                return;
            }
            if (dpStartDate.SelectedDate == null) {
                MessageBox.Show("Please select a start date.");
                return;
            }
            if (dpEndDate.SelectedDate == null) {
                MessageBox.Show("Please select a end date.");
                return;
            }
            if(dpStartDate.SelectedDate > dpEndDate.SelectedDate) {
                MessageBox.Show("Sprint can't end before it begins silly.");
                return;
            }


            // Need to check if a sprint is active during selected time period
            // Or if there is a sprint that already has the selected feature
            Feature feature = (Feature)cboFeature.SelectedItem;
            List<Sprint> sprints = sprintManager.GetSprintsByProjectID(projectID);


            foreach (Sprint sprint in sprints) {
                if (sprint.FeatureID == feature.FeatureID) {
                    MessageBox.Show("Sprint already exists for the feature you selected");
                    return;
                }
            }

            // Passed all validation
            try {
                Sprint sprint = new Sprint() {
                    FeatureID = feature.FeatureID,
                    StartDate = (DateTime)dpStartDate.SelectedDate,
                    EndDate = (DateTime)dpEndDate.SelectedDate
                };
                if (sprintManager.AddSprint(sprint)) {
                    this.DialogResult = true;
                } else {
                    this.DialogResult = false;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboFeature_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cboFeature.SelectedItem != null) {
                FeatureVM featureVM = cboFeature.SelectedItem as FeatureVM;
                try {
                    lvFeatureStories.ItemsSource = userStoryManager.GetFeatureUserStories(featureVM.FeatureID);
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
