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
        int taskID;
        FileManager fileManager = new FileManager();
        public TaskView(int taskID) {
            this.taskID = taskID;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            tabUseCase.Content = new FileUploadView(taskID, "UseCase");
            tabStoredProcedure.Content = new FileUploadView(taskID, "Stored Procedure Specification");
            tabInterfaces.Content = new FileUploadView(taskID, "User Interface");
            tabERDiagram.Content = new FileUploadView(taskID, "ER Diagram");
            tabDD.Content = new FileUploadView(taskID, "Data Dictionary");
            tabDM.Content = new FileUploadView(taskID, "Data Model");
        }
    }
}
