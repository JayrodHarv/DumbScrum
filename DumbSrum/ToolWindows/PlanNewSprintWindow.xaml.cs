using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DumbSrum.ToolWindows {
    /// <summary>
    /// Interaction logic for PlanNewSprint.xaml
    /// </summary>
    public partial class PlanNewSprintWindow : Window {
        string projectID = string.Empty;
        List<SprintVM> sprints = new List<SprintVM>();
        FeatureManager featureManager = new FeatureManager();
        UserStoryManager userStoryManager = new UserStoryManager();
        SprintManager sprintManager = new SprintManager();
        TaskManager taskManager = new TaskManager();

        public PlanNewSprintWindow(string projectID) {
            this.projectID = projectID;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            sprints = sprintManager.GetSprintVMsByProjectID(projectID);
            List<FeatureVM> features = featureManager.GetFeaturesByProjectID(projectID);
            cboFeature.ItemsSource = features;

            // preview calendar
            foreach(SprintVM sprint in sprints) {
                dpStartDate.BlackoutDates.AddDatesInPast();
                dpStartDate.BlackoutDates.Add(new CalendarDateRange(sprint.StartDate, sprint.EndDate));
                dpEndDate.BlackoutDates.AddDatesInPast();
                dpEndDate.BlackoutDates.Add(new CalendarDateRange(sprint.StartDate, sprint.EndDate));
            }
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

            Feature feature = (Feature)cboFeature.SelectedItem;
            foreach (Sprint sprint in sprints) {
                if (sprint.FeatureID == feature.FeatureID) {
                    MessageBox.Show("Sprint already exists for the feature you selected");
                    return;
                }
            }

            // Passed all validation
            try {
                if (sprintManager.AddSprint(new Sprint() {
                    FeatureID = feature.FeatureID,
                    StartDate = (DateTime)dpStartDate.SelectedDate,
                    EndDate = (DateTime)dpEndDate.SelectedDate
                })) {
                    sprints = sprintManager.GetSprintVMsByProjectID(projectID);
                    // Create a task for every user story from sprint feature
                    List<UserStory> stories = userStoryManager.GetFeatureUserStories(feature.FeatureID);
                    Sprint sprint = sprintManager.GetSprintVMByFeatureID(feature.FeatureID);
                    
                    foreach(UserStory story in stories) {
                        taskManager.CreateTask(sprint.SprintID, story.StoryID, "Unclaimed");
                    }
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
