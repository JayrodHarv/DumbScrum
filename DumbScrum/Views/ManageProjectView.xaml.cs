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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DumbScrum.Views {
    /// <summary>
    /// Interaction logic for ManageProjectView.xaml
    /// </summary>
    public partial class ManageProjectView : UserControl {
        ProjectManager projectManager = new ProjectManager();
        string projectID;
        public ManageProjectView(string projectID) {
            this.projectID = projectID;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            tabSettings.Content = new ProjectSettingsView(projectID);
        }
    }
}
