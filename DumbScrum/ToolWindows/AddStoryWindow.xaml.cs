using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DumbScrum {
    /// <summary>
    /// Interaction logic for AddStoryWindow.xaml
    /// </summary>
    public partial class AddStoryWindow : Window {
        TaskManager taskManager = new TaskManager();
        SprintManager sprintManager = new SprintManager();
        Feature feature;
        public AddStoryWindow(Feature feature) {
            this.feature = feature;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }

        private void btnAddStory_Click(object sender, RoutedEventArgs e) {
            if (txtPerson.Text == "") {
                MessageBox.Show("A user story must have a user.");
                return;
            }
            if (txtAction.Text == "") {
                MessageBox.Show("You must enter an action.");
                return;
            }
            if (txtReason.Text == "") {
                MessageBox.Show("You must give a reason.");
                return;
            }

            UserStoryManager userStoryManager = new UserStoryManager();
            List<UserStory> stories = userStoryManager.GetFeatureUserStories(feature.FeatureID);

            UserStory story = new UserStory() {
                StoryID = feature.FeatureID + "." + (stories.Count + 1),
                FeatureID = feature.FeatureID, 
                Person = txtPerson.Text.ToLower(), 
                Action = txtAction.Text.ToLower(), 
                Reason = txtReason.Text.ToLower()
            };
            try {
                if (userStoryManager.AddFeatureUserStory(story)) {
                    if (feature.Status == "Currently In Sprint") {
                        SprintVM sprintVM = sprintManager.GetSprintVMByFeatureID(feature.FeatureID);
                        Task task = new Task() {
                            SprintID = sprintVM.SprintID,
                            StoryID = story.StoryID,
                            Status = "To Do"
                        };
                        taskManager.CreateTask(task);
                        MessageBox.Show("Since this feature is a part of an ongoing sprint, a task was created for the sprint using the newly created user story.");
                    }
                    this.DialogResult = true;
                } else {
                    MessageBox.Show("Failed to add user story to feature.");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
