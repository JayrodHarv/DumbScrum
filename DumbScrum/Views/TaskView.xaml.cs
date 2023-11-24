using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace DumbScrum.Views {
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : UserControl {
        int taskID;
        public TaskView(int taskID) {
            this.taskID = taskID;
            InitializeComponent();
        }

        private void btnUploadUseCaseFile_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Word Doc | *.docx";
            bool? result = fileDialog.ShowDialog();
            if (result == true) {
                string path = fileDialog.FileName;
            } else {

            }
        }
    }
}
