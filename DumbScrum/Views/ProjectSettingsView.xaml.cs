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
using DataObjects;
using DumbScrum.UserControls;

namespace DumbScrum.Views {
    /// <summary>
    /// Interaction logic for ProjectSettingsView.xaml
    /// </summary>
    public partial class ProjectSettingsView : UserControl {
        string projectID;
        FileManager fileManager = new FileManager();
        List<File> templateFiles = new List<File>();
        public ProjectSettingsView(string projectID) {
            this.projectID = projectID;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            icTemplateFiles.Items.Clear();
            icTemplateFiles.Items.Add(new TemplateFilePicker(projectID, "Use Case"));
            icTemplateFiles.Items.Add(new TemplateFilePicker(projectID, "Stored Procedure Specification"));
            icTemplateFiles.Items.Add(new TemplateFilePicker(projectID, "User Interface"));
            icTemplateFiles.Items.Add(new TemplateFilePicker(projectID, "ER Diagram"));
            icTemplateFiles.Items.Add(new TemplateFilePicker(projectID, "Data Dictionary"));
            icTemplateFiles.Items.Add(new TemplateFilePicker(projectID, "Data Model"));
        }

        private string Change(string type, string startText) {
            string filter = GetFileFilter(type);
            
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = filter;
            bool? result = fileDialog.ShowDialog();
            if (result == true) {
                try {
                    // get the old file from list of template files
                    File oldFile = templateFiles.Find(f => f.Type == type);

                    // get the new file
                    File newFile = GetFile(fileDialog.FileName, type);

                    if (oldFile == null) {
                        // use insert sp instead
                        fileManager.AddTemplateFile(newFile);
                    } else {
                        fileManager.EditFile(oldFile, newFile);
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
               
                return fileDialog.SafeFileName;
            }
            return startText;
        }

        private File GetFile(string filePath, string type) {
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
