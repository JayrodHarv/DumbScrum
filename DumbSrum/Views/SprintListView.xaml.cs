﻿using DataObjects;
using DumbSrum.ToolWindows;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DumbSrum.Views {
    /// <summary>
    /// Interaction logic for SprintListView.xaml
    /// </summary>
    public partial class SprintListView : UserControl {
        string projectID;
        SprintManager sprintManager = new SprintManager();
        List<SprintVM> sprints = new List<SprintVM>();
        public SprintListView(string projectID) {
            this.projectID = projectID;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            sprints = sprintManager.GetSprintVMsByProjectID(projectID);
            lvSprints.ItemsSource = sprints;

            // preview calendar
            foreach (SprintVM sprint in sprints) {
                calSprint.BlackoutDates.Add(new CalendarDateRange(sprint.StartDate, sprint.EndDate));
            }
        }

        private void tabSprintList_GotFocus(object sender, RoutedEventArgs e) {
            sprints = sprintManager.GetSprintVMsByProjectID(projectID);
            lvSprints.ItemsSource = sprints;
        }

        private void btnPlanNewSprint_Click(object sender, RoutedEventArgs e) {
            try {
                var planNewSprintWindow = new PlanNewSprintWindow(projectID);
                var result = planNewSprintWindow.ShowDialog();
                if (result == true) {
                    MessageBox.Show("Sprint Successfully Planned.", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    // Automatically create a task for each user story of the sprint feature


                    lvSprints.ItemsSource = sprintManager.GetSprintVMsByProjectID(projectID);
                } else {
                    MessageBox.Show("Sprint Not Planned", "Operation Aborted",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                    "Failed To Plan Sprint", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}