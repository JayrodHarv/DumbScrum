using DataObjects;
using LogicLayer;
using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DumbScrum.UserControls {
    /// <summary>
    /// Interaction logic for TemplateFilePicker.xaml
    /// </summary>
    public partial class TemplateFilePicker : UserControl {
        FileManager fileManager = new FileManager();
        string projectID;
        string type;
        public TemplateFilePicker(string projectID, string type) {
            this.projectID = projectID;
            this.type = type;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            tbType.Text = type + " Template File:";
            try {
                File f = fileManager.GetTemplateFile(projectID, type);
                if(f != null) {
                    tbFile.Text = f.FileName;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = GetFileFilter(type);
            bool? result = fileDialog.ShowDialog();
            if (result == true) {
                try {
                    // get the old template file
                    File oldFile = fileManager.GetTemplateFile(projectID, type);

                    // get the new file
                    File newFile = GetFile(fileDialog.FileName);

                    if (oldFile == null) {
                        // use insert sp instead
                        fileManager.AddTemplateFile(newFile);
                        tbFile.Text = fileDialog.SafeFileName;
                    } else {
                        if(fileManager.EditFile(oldFile, newFile)) {
                            tbFile.Text = fileDialog.SafeFileName;
                            MessageBox.Show("Sucessfully Updated Template File");
                        }
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private File GetFile(string filePath) {
            using (System.IO.Stream stream = System.IO.File.OpenRead(filePath)) {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                string extn = new System.IO.FileInfo(filePath).Extension;
                string fileName = new System.IO.FileInfo(filePath).Name;

                File file = new File() {
                    Data = buffer,
                    Extension = extn,
                    ProjectID = projectID,
                    FileName = fileName,
                    Type = type,
                    LastEdited = DateTime.Now
                };
                return file;
            }
        }

        private string GetFileFilter(string type) {
            switch (type) {
                case "Use Case":
                    return "Word Doc | *.docx";
                case "Stored Procedure Specification":
                    return "Word Doc | *.docx";
                case "User Interface":
                    return "Pencil File | *.epgz";
                case "ER Diagram":
                    return "Diagrams.net | *.drawio";
                case "Data Dictionary":
                    return "Excel | *.xlsx";
                case "Data Model":
                    return "Diagrams.net | *.drawio";
                default:
                    return "";
            }
        }
    }
}
