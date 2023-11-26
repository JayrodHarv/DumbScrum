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
        }
    }
}
