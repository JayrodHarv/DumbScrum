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
        TaskVM task;
        public CreateNewTaskFileWindow(int taskID, string type, File template) {
            this.taskID = taskID;
            this.type = type;
            this.template = template;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            btnCreateFile.IsDefault = true;
            try {
                task = taskManager.GetTask(taskID);
                tbFilePrefix.Text = task.StoryID + "-" + GetTypeShort(type) + "-";
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreateFile_Click(object sender, RoutedEventArgs e) {
            if(tbFileName.Text == "") {
                MessageBox.Show("You must name the file.");
                return;
            }
            try {
                File file = new File() {
                    FileName = tbFilePrefix.Text + tbFileName.Text + template.Extension,
                    Data = template.Data,
                    Extension = template.Extension,
                    TaskID = taskID,
                    Type = type,
                    LastEdited = DateTime.Now
                };
                if(fileManager.AddTaskFile(file)) {
                    this.DialogResult = true;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }

        private string GetTypeShort(string type) {
            switch (type) {
                case "Use Case":
                    return "UC";
                case "Stored Procedure Specification":
                    return "SP";
                case "User Interface":
                    return "UI";
                case "ER Diagram":
                    return "ER";
                case "Data Dictionary":
                    return "DD";
                case "Data Model":
                    return "DM";
                default:
                    return "";
            }
        }
    }
}
