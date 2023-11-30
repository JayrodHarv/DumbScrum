using DataObjects;
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
using System.Windows.Shapes;

namespace DumbScrum.ToolWindows {
    /// <summary>
    /// Interaction logic for CreateNewTaskFileWindow.xaml
    /// </summary>
    public partial class CreateNewTaskFileWindow : Window {
        FileManager fileManager = new FileManager();
        TaskManager taskManager = new TaskManager();
        int taskID;
        string type;
        File template;
        public CreateNewTaskFileWindow(int taskID, string type, File template) {
            this.taskID = taskID;
            this.type = type;
            this.template = template;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                //tbFilePrefix.Text = taskManager.GetTaskByTaskID()
            } catch (Exception) {

                throw;
            }
        }

        private void btnCreateFile_Click(object sender, RoutedEventArgs e) {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {

        }

        // get template for the current type

        // make a copy of it and save to database

        // update it when edits are made
    }
}
