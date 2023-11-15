﻿using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DumbSrum {
    /// <summary>
    /// Interaction logic for AddFeatureWindow.xaml
    /// </summary>
    public partial class AddFeatureWindow : Window {
        string _projectID = string.Empty;
        public AddFeatureWindow(string projectID) {
            _projectID = projectID;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }

        private void btnAddFeature_Click(object sender, RoutedEventArgs e) {
            if (txtFeatureName.Text == "") {
                MessageBox.Show("You must give the feature a name.");
                return;
            }
            if(txtDescription.Text == "") {
                MessageBox.Show("You must add a description to the feature.");
                return;
            }

            if (cboPriority.SelectedItem == null) {
                MessageBox.Show("You must chose a priority for the feature.");
                return;
            }

            FeatureManager featureManager = new FeatureManager();

            List<Feature> features = featureManager.GetFeaturesByProjectID(_projectID);
            
            foreach (Feature feature in features) {
                if(feature.Name == txtFeatureName.Text) {
                    MessageBox.Show("Feature already exists with the chosen name. Please call it something else.");
                    return;
                }
            }

            // everything is good
            try {
                if(featureManager.AddProjectFeature(_projectID, txtFeatureName.Text, txtDescription.Text, cboPriority.Text)) {
                    this.DialogResult = true;
                } else {
                    MessageBox.Show("Failed to add feature.");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
