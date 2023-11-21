using LogicLayer;
using System;
using System.Windows;

namespace DumbSrum {
    /// <summary>
    /// Interaction logic for AddStoryWindow.xaml
    /// </summary>
    public partial class AddStoryWindow : Window {
        int featureID;
        public AddStoryWindow(int featureID) {
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

            try {
                if (userStoryManager.AddFeatureUserStory(featureID, txtPerson.Text.ToLower(), txtAction.Text.ToLower(), txtReason.Text.ToLower())) {
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
