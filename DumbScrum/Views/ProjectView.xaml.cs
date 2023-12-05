using DataObjects;
using LogicLayer;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DumbScrum.Views {
    /// <summary>
    /// Interaction logic for ProjectView.xaml
    /// </summary>
    public partial class ProjectView : UserControl, INotifyPropertyChanged {
        ProjectManager _projectManager = new ProjectManager();
        public ProjectVM _projectVM { get; set; }
        UserVM user;

        private object _currentProjectView;

        public object CurrentProjectView {
            get { return _currentProjectView; }
            set {
                _currentProjectView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentProjectView"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ProjectView(string projectID, UserVM user) {
            DataContext = this;
            this.user = user;
            try {
                _projectVM = _projectManager.GetProjectVMByProjectID(projectID);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            InitializeComponent();
            if (_projectVM.UserID == user.UserID) {
                btnManage.Visibility = Visibility.Visible;
                CurrentProjectView = new ManageProjectView(_projectVM.ProjectID);
            } else {
                CurrentProjectView = new ProjectFeedView(_projectVM.ProjectID, user.UserID);
                btnFeed.IsChecked = true;
            }
        }

        private void btnFeed_Click(object sender, RoutedEventArgs e) {
            CurrentProjectView = new ProjectFeedView(_projectVM.ProjectID, user.UserID);
        }

        private void btnBacklog_Click(object sender, RoutedEventArgs e) {
            CurrentProjectView = new BacklogView(_projectVM.ProjectID);
        }

        private void btnBoard_Click(object sender, RoutedEventArgs e) {
            CurrentProjectView = new BoardView(_projectVM.ProjectID, user);
        }

        private void btnSprints_Click(object sender, RoutedEventArgs e) {
            CurrentProjectView = new SprintListView(_projectVM.ProjectID);
        }

        private void btnManage_Click(object sender, RoutedEventArgs e) {
            CurrentProjectView = new ManageProjectView(_projectVM.ProjectID);
        }
    }
}
