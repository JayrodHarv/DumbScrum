using DataObjects;
using LogicLayer;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace DumbScrum.Views {
    /// <summary>
    /// Interaction logic for ProjectView.xaml
    /// </summary>
    public partial class BacklogView : UserControl {
        FeatureManager featureManager = new FeatureManager();
        UserStoryManager userStoryManager = new UserStoryManager();
        string _projectID = string.Empty;
        public ObservableCollection<Feature> Features { get; set; }
        public BacklogView(string projectID) {
            DataContext = this;
            _projectID = projectID;

            UpdateFeatureList();

            InitializeComponent();
        }

        private void UpdateFeatureList() {
            try {
                Features = new ObservableCollection<Feature>(featureManager.GetFeaturesByProjectID(_projectID));
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddFeature_Click(object sender, RoutedEventArgs e) {
            try {
                var addFeatureWindow = new AddFeatureWindow(_projectID);
                var result = addFeatureWindow.ShowDialog();
                if (result == true) {
                    MessageBox.Show("Feature Successfully Added.", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateFeatureList();
                    lvFeatures.ItemsSource = Features;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                    "Failed To Add Feature", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddStory_Click(object sender, RoutedEventArgs e) {
            if(lvFeatures.SelectedItem != null) {
                try {
                    Feature selectedFeature = lvFeatures.SelectedItem as Feature;
                    var addStoryWindow = new AddStoryWindow(selectedFeature);
                    var result = addStoryWindow.ShowDialog();
                    if (result == true) {
                        MessageBox.Show("User Story Successfully Added.", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        lvStories.ItemsSource = userStoryManager.GetFeatureUserStories(selectedFeature.FeatureID);
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "Failed To Add User Story", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } else {
                MessageBox.Show("You must select a feature from the list on the right in order to add a user story to it.");
            }
        }

        private void lvFeatures_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                Feature selectedFeature = lvFeatures.SelectedItem as Feature;
                if (selectedFeature != null) {
                    lvStories.ItemsSource = userStoryManager.GetFeatureUserStories(selectedFeature.FeatureID);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
