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

namespace DumbScrum.Views {
    /// <summary>
    /// Interaction logic for ProjectSettingsView.xaml
    /// </summary>
    public partial class ProjectSettingsView : UserControl {
        string projectID;
        public ProjectSettingsView(string projectID) {
            this.projectID = projectID;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            
        }

        private void btnUseCaseChange_Click(object sender, RoutedEventArgs e) {
            tbUseCaseFile.Text = Change("Use Case");
        }

        private string Change(string type) {
            string filter = GetFileFilter(type);
            
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = filter;
            bool? result = fileDialog.ShowDialog();
            if (result == true) {

                return fileDialog.SafeFileName;
            }
            return "";
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
