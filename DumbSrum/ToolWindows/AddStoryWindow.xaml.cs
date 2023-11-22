using DataObjects;
using LogicLayer;
using System;
using System.Windows;

namespace DumbSrum {
    /// <summary>
    /// Interaction logic for AddStoryWindow.xaml
    /// </summary>
    public partial class AddStoryWindow : Window {
        string featureID;
        public AddStoryWindow(string featureID) {
            InitializeComponent();
            this.featureID = featureID;
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
            UserStory story = new UserStory() {
                FeatureID = featureID, 
                Person = txtPerson.Text.ToLower(), 
                Action = txtAction.Text.ToLower(), 
                Reason = txtReason.Text.ToLower()
            };
            try {
                if (userStoryManager.AddFeatureUserStory(story)) {
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
