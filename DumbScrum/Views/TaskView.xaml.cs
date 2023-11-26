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

        private void btnUploadUseCaseFile_Click(object sender, RoutedEventArgs e) {
            string type = "UseCase";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Word Doc | *.docx";
            bool? result = fileDialog.ShowDialog();
            if (result == true) {
                string path = fileDialog.FileName;
                SaveFile(path, type);
                dgUseCases.ItemsSource = fileManager.GetTaskFilesByType(taskID, type);
            } else {

            }
        }

        private void SaveFile(string filePath, string type) {
            using(Stream stream = File.OpenRead(filePath)) {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                string extn = new FileInfo(filePath).Extension;
                string fileName = new FileInfo(filePath).Name;

                DataObjects.File file = new DataObjects.File() {
                    Data = buffer,
                    Extension = extn,
                    TaskID = taskID,
                    FileName = fileName,
                    Type = type
                };
                try {
                    if(fileManager.AddFile(file)) {
                        MessageBox.Show("File Successfully Added.");
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void OpenFile() {

        }

        private void tabUseCase_GotFocus(object sender, RoutedEventArgs e) {
            try {
                dgUseCases.ItemsSource = fileManager.GetTaskFilesByType(taskID, "UseCase");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
