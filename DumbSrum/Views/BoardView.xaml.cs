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
                cbxSprint.ItemsSource = sprintManager.GetSprintVMsByProjectID(projectID);
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

        private void btnAddTask_Click(object sender, RoutedEventArgs e) {

        }
    }
}
