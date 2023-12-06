using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DumbScrum.ToolWindows {
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
        bool isEditing = false;
        SprintVM sprintVM;

        public PlanNewSprintWindow(string projectID) {
            this.projectID = projectID;
            InitializeComponent();
        }
        public PlanNewSprintWindow(string projectID, SprintVM sprintVM) {
            this.projectID = projectID;
            this.sprintVM = sprintVM;
            isEditing = true;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            btnPlanSprint.IsDefault = true;
            List<FeatureVM> features = featureManager.GetFeaturesByProjectID(projectID);
            cboFeature.ItemsSource = features;
            sprints = sprintManager.GetSprintVMsByProjectID(projectID);
            if (!isEditing) {
                // preview calendar
                foreach (SprintVM sprint in sprints) {
                    dpStartDate.BlackoutDates.AddDatesInPast();
                    dpStartDate.BlackoutDates.Add(new CalendarDateRange(sprint.StartDate, sprint.EndDate));
                    dpEndDate.BlackoutDates.AddDatesInPast();
                    dpEndDate.BlackoutDates.Add(new CalendarDateRange(sprint.StartDate, sprint.EndDate));
                }
            } else {
                // preview calendar
                foreach (SprintVM sprint in sprints) {
                    if(sprint.SprintID != sprintVM.SprintID) {
                        dpStartDate.BlackoutDates.AddDatesInPast();
                        dpStartDate.BlackoutDates.Add(new CalendarDateRange(sprint.StartDate, sprint.EndDate));
                        dpEndDate.BlackoutDates.AddDatesInPast();
                        dpEndDate.BlackoutDates.Add(new CalendarDateRange(sprint.StartDate, sprint.EndDate));
                    }
                }
                Feature feature = features.Find(f => f.FeatureID == sprintVM.FeatureID);
                cboFeature.SelectedItem = feature;
                cboFeature.IsEnabled = false;
                tbSprintName.Text = sprintVM.Name;
                dpStartDate.SelectedDate = sprintVM.StartDate;
                dpEndDate.SelectedDate = sprintVM.EndDate;
                btnPlanSprint.Content = "Edit Sprint Schedule";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }

        private void btnPlanSprint_Click(object sender, RoutedEventArgs e) {
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

            if(!isEditing) {
                if (cboFeature.SelectedItem == null) {
                    MessageBox.Show("Please select a feature to add to the sprint.");
                    return;
                }
                if (tbSprintName.Text == "") {
                    MessageBox.Show("You must give the sprint a name.");
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
                        Name = tbSprintName.Text,
                        FeatureID = feature.FeatureID,
                        StartDate = (DateTime)dpStartDate.SelectedDate,
                        EndDate = (DateTime)dpEndDate.SelectedDate
                    })) {
                        sprints = sprintManager.GetSprintVMsByProjectID(projectID);
                        // Create a task for every user story from sprint feature
                        List<UserStory> stories = userStoryManager.GetFeatureUserStories(feature.FeatureID);
                        Sprint sprint = sprintManager.GetSprintVMByFeatureID(feature.FeatureID);

                        foreach (UserStory story in stories) {
                            Task task = new Task() {
                                SprintID = sprint.SprintID,
                                StoryID = story.StoryID,
                                Status = "To Do"
                            };
                            taskManager.CreateTask(task);
                        }
                        this.DialogResult = true;
                    } else {
                        this.DialogResult = false;
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            } else {
                Sprint newSprint = new Sprint() {
                    SprintID = sprintVM.SprintID,
                    Name = tbSprintName.Text,
                    StartDate = (DateTime)dpStartDate.SelectedDate,
                    EndDate = (DateTime)dpEndDate.SelectedDate
                };
                try {
                    if(sprintManager.EditSprint(newSprint, sprintVM)) {
                        this.DialogResult = true;
                    } else {
                        this.DialogResult = false;
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
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
