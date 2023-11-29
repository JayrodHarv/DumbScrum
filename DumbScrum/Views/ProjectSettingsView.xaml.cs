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
            try {
                templateFiles = fileManager.GetProjectTemplateFiles(projectID);
                foreach (File f in templateFiles) {
                    switch (f.Type) {
                        case "Use Case":
                            tbUseCaseFile.Text = f.FileName;
                            break;
                        case "Stored Procedure Specification":
                            tbStoredProcedureFile.Text = f.FileName;
                            break;
                        case "User Interface":
                            tbUserInterfaceFile.Text = f.FileName;
                            break;
                        case "ER Diagram":
                            tbERDiagramFile.Text = f.FileName;
                            break;
                        case "Data Dictionary":
                            tbDataDictionaryFile.Text = f.FileName;
                            break;
                        case "Data Model":
                            tbDataModelFile.Text = f.FileName;
                            break;
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUseCaseChange_Click(object sender, RoutedEventArgs e) {
            tbUseCaseFile.Text = Change("Use Case", tbUseCaseFile.Text);
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

        private void btnStoredProcedureChange_Click(object sender, RoutedEventArgs e) {
            tbStoredProcedureFile.Text = Change("Stored Procedure Specification", tbStoredProcedureFile.Text);
        }

        private void btnUserInterfaceChange_Click(object sender, RoutedEventArgs e) {
            tbUserInterfaceFile.Text = Change("User Interface", tbUserInterfaceFile.Text);
        }

        private void btnERDiagramChange_Click(object sender, RoutedEventArgs e) {
            tbERDiagramFile.Text = Change("ER Diagram", tbERDiagramFile.Text);
        }

        private void btnDataDictionaryChange_Click(object sender, RoutedEventArgs e) {

        }

        private void btnDataModelChange_Click(object sender, RoutedEventArgs e) {

        }
    }
}
