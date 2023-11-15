using DataObjects;
using LogicLayer;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DumbSrum.Views {
    /// <summary>
    /// Interaction logic for ProjectView.xaml
    /// </summary>
    public partial class ProjectView : UserControl, INotifyPropertyChanged {
        ProjectManager _projectManager = new ProjectManager();
        public ProjectVM _projectVM { get; set; }


        private object _currentProjectView;

        public object CurrentProjectView {
            get { return _currentProjectView; }
            set {
                _currentProjectView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentProjectView"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ProjectView(string projectID) {
            DataContext = this;
            try {
                _projectVM = _projectManager.GetProjectVMByProjectID(projectID);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            CurrentProjectView = new ProjectDashboardView();
            InitializeComponent();
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e) {
            CurrentProjectView = new ProjectDashboardView();
        }

        private void btnFeed_Click(object sender, RoutedEventArgs e) {
            CurrentProjectView = new ProjectFeedView();
        }

        private void btnBacklog_Click(object sender, RoutedEventArgs e) {
            CurrentProjectView = new BacklogView(_projectVM.ProjectID);
        }

        private void btnBoard_Click(object sender, RoutedEventArgs e) {
            CurrentProjectView = new BoardView(_projectVM.ProjectID);

        }

        private void btnIssues_Click(object sender, RoutedEventArgs e) {

        }
    }
}
