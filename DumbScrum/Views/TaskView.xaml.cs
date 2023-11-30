using LogicLayer;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DumbScrum.Views {
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : UserControl {
        string projectID;
        int taskID;
        FileManager fileManager = new FileManager();
        public TaskView(string projectID, int taskID) {
            this.projectID = projectID;
            this.taskID = taskID;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            tabUseCase.Content = new FileUploadView(projectID, taskID, "Use Case");
            tabStoredProcedure.Content = new FileUploadView(projectID, taskID, "Stored Procedure Specification");
            tabInterfaces.Content = new FileUploadView(projectID, taskID, "User Interface");
            tabERDiagram.Content = new FileUploadView(projectID, taskID, "ER Diagram");
            tabDD.Content = new FileUploadView(projectID, taskID, "Data Dictionary");
            tabDM.Content = new FileUploadView(projectID, taskID, "Data Model");
        }
    }
}
