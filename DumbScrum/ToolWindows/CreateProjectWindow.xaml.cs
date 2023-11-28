﻿using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DumbScrum {
    /// <summary>
    /// Interaction logic for CreateProjectWindow.xaml
    /// </summary>
    public partial class CreateProjectWindow : Window {
        User user;
        public CreateProjectWindow(User user) {
            InitializeComponent();
            this.user = user;
        }

        private void btnCreateProject_Click(object sender, RoutedEventArgs e) {
            if(txtProjectTitle.Text == "") {
                MessageBox.Show("You have to give your project a name.");
                return;
            }
            if(txtDescription.Text == "") {
                MessageBox.Show("Your project must have a description.");
            }

            ProjectManager projectManager = new ProjectManager();

            List<ProjectVM> projects = projectManager.GetAllProjects();

            foreach (ProjectVM p in projects) {
                if (p.ProjectID == txtProjectTitle.Text) {
                    MessageBox.Show("Project already exists with the chosen name. Please call it something else.");
                    return;
                }
            }

            Project project = new Project() {
                ProjectID = txtProjectTitle.Text, 
                UserID = user.UserID, 
                Description = txtDescription.Text, 
            };

            try {
                if (projectManager.AddProject(project)) {
                    this.DialogResult = true;
                } else {
                    MessageBox.Show("Failed to create project.");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }
    }
}
