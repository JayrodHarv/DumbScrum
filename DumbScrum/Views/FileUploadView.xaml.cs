using DataObjects;
using LogicLayer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

namespace DumbScrum.Views {
    public partial class FileUploadView : UserControl {
        FileManager fileManager = new FileManager();
        string type;
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
        }

        private void SaveFile(string filePath) {
            try {
                DataObjects.File file = GetFile(filePath);
                if (fileManager.AddFile(file)) {
                    MessageBox.Show("File Successfully Added.");
                    lvFiles.ItemsSource = fileManager.GetTaskFilesByType(taskID, type);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Word Doc | *.docx";
            bool? result = fileDialog.ShowDialog();
            if (result == true) {
                tbFilePath.Text = fileDialog.FileName;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e) {
            if(tbFilePath.Text != "") {
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
                    Type = type
                };
                return file;
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e) {
            DataObjects.File selectedFile = lvFiles.SelectedItem as DataObjects.File;
            System.IO.File.WriteAllBytes(@"Documents\" + selectedFile.FileName, selectedFile.Data);
            using(var process = new System.Diagnostics.Process()) {
                process.StartInfo.FileName = @"Documents\" + selectedFile.FileName;
                process.Start();
                if(process.HasExited) {
                    // get the new file
                    DataObjects.File newFile = GetFile(@"Documents\" + selectedFile.FileName);
                    // update the file in the database
                    try {
                        if(fileManager.EditFile(selectedFile, newFile)) {
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
            try {
                if(fileManager.RemoveFile(selectedFile.FileID)) {
                    MessageBox.Show("File Successfully Deleted.");
                    lvFiles.ItemsSource = fileManager.GetTaskFilesByType(taskID, type);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
