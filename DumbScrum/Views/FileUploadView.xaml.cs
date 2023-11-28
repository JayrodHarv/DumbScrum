using DumbScrum.ToolWindows;
using LogicLayer;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DumbScrum.Views {
    public partial class FileUploadView : UserControl {
        FileManager fileManager = new FileManager();
        string type;
        string filter;
        int taskID;
        public FileUploadView(int taskID, string type) {
            this.type = type;
            this.taskID = taskID;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            try {
                lvFiles.ItemsSource = fileManager.GetTaskFilesByType(taskID, type);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            switch (type) {
                case "Use Case":
                    filter = "Word Doc | *.docx";
                    break;
                case "Stored Procedure Specification":
                    filter = "Word Doc | *.docx";
                    break;
                case "User Interface":
                    filter = "Pencil File | *.epgz";
                    break;
                case "ER Diagram":
                    filter = "Diagrams.net | *.drawio";
                    break;
                case "Data Dictionary":
                    filter = "Excel | *.xlsx";
                    break;
                case "Data Model":
                    filter = "Diagrams.net | *.drawio";
                    break;
            }
        }

        private void SaveFile(string filePath) {
            try {
                DataObjects.File file = GetFile(filePath);
                if (fileManager.AddFile(file)) {
                    MessageBox.Show("File Successfully Added.");
                    lvFiles.ItemsSource = fileManager.GetTaskFilesByType(taskID, type);
                    tbFilePath.Text = "";
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = filter;
            bool? result = fileDialog.ShowDialog();
            if (result == true) {
                tbFilePath.Text = fileDialog.FileName;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e) {
            if (tbFilePath.Text != "") {
                SaveFile(tbFilePath.Text);
            } else {
                MessageBox.Show("Please select a file.");
            }
        }

        private DataObjects.File GetFile(string filePath) {
            using (Stream stream = System.IO.File.OpenRead(filePath)) {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                string extn = new FileInfo(filePath).Extension;
                string fileName = new FileInfo(filePath).Name;

                DataObjects.File file = new DataObjects.File() {
                    Data = buffer,
                    Extension = extn,
                    TaskID = taskID,
                    FileName = fileName,
                    Type = type,
                    LastEdited = DateTime.Now
                };
                return file;
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e) {
            DataObjects.File selectedFile = lvFiles.SelectedItem as DataObjects.File;
            string filePath = selectedFile.FileName;
            System.IO.File.WriteAllBytes(filePath, selectedFile.Data);
            using (var process = new System.Diagnostics.Process()) {
                process.StartInfo.FileName = filePath;
                process.Start();
                process.WaitForExit();
                if (process.HasExited) {
                    // get the new file
                    DataObjects.File newFile = GetFile(filePath);
                    // update the file in the database
                    try {
                        if (fileManager.EditFile(selectedFile, newFile)) {
                            System.IO.File.Delete(filePath);
                            MessageBox.Show("File Updated");
                            lvFiles.ItemsSource = fileManager.GetTaskFilesByType(taskID, type);
                        }
                    } catch (Exception ex) {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            DataObjects.File selectedFile = lvFiles.SelectedItem as DataObjects.File;
            if(selectedFile != null) {
                var result = MessageBox.Show("Are you sure that you want to permanently delete " + selectedFile.FileName, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes) {
                    try {
                        if (fileManager.RemoveFile(selectedFile.FileID)) {
                            MessageBox.Show("File Successfully Deleted.");
                            lvFiles.ItemsSource = fileManager.GetTaskFilesByType(taskID, type);
                        }
                    } catch (Exception ex) {
                        MessageBox.Show(ex.Message);
                    }
                }
            } else {
                MessageBox.Show("You must select a file in order to delete it.");
            }
        }

        private void btnCreateNew_Click(object sender, RoutedEventArgs e) {
            // open CreateNewTaskFileWindow
            try {
                CreateNewTaskFileWindow createNewTaskFileWindow = new CreateNewTaskFileWindow(taskID, type);
                bool? result = createNewTaskFileWindow.ShowDialog();
                if (result == true) {
                    MessageBox.Show("File was created using the project's " + type + " template.", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                } else {
                    MessageBox.Show("File Not Created", "Operation Aborted",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                    "Failed To Create File", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // get template for the current type

            // make a copy of it and save to database

            // update it when edits are made
        }
    }
}
