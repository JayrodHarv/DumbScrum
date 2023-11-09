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
        FeatureManager _featureManager = new FeatureManager();
        UserStoryManager _userStoryManager = new UserStoryManager();
        public ProjectVM _projectVM { get; set; }
        public ObservableCollection<Feature> Features { get; set; }
        public ObservableCollection<UserStory> UserStories { get; set; }
        


        public BacklogView backlogView { get; set; }
        public BoardView boardView { get; set; }
        public ProjectFeedView feedView { get; set; }
        public ProjectDashboardView dashboardView { get; set; }


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

                _projectVM.Features = _featureManager.GetFeaturesByProjectID(_projectVM.ProjectID);
                Features = new ObservableCollection<Feature>(_projectVM.Features);

                // _projectVM.Sprints = _sprintManager.GetSprintsByProjectID(_projectVM.ProjectID);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            backlogView = new BacklogView();
            feedView = new ProjectFeedView();
            dashboardView = new ProjectDashboardView();

            CurrentProjectView = dashboardView;
            InitializeComponent();
            // GetFeatureUserStories(); User Stories should be stored in a FeatureVM I think
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e) {
            CurrentProjectView = dashboardView;
        }

        private void btnFeed_Click(object sender, RoutedEventArgs e) {
            CurrentProjectView = feedView;
        }

        private void btnBacklog_Click(object sender, RoutedEventArgs e) {
            CurrentProjectView = backlogView;
        }

        private void btnBoard_Click(object sender, RoutedEventArgs e) {
            CurrentProjectView = new BoardView(_projectVM.ProjectID);

        }

        private void btnIssues_Click(object sender, RoutedEventArgs e) {

        }
    }
}
