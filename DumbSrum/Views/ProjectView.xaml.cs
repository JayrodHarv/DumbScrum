using DataObjects;
using LogicLayer;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace DumbSrum.Views {
    /// <summary>
    /// Interaction logic for ProjectView.xaml
    /// </summary>
    public partial class ProjectView : UserControl, INotifyPropertyChanged {
        FeatureManager _featureManager = new FeatureManager();
        public Project _project { get; set; }
        public ObservableCollection<Feature> Features { get; set; }
        
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

        public ProjectView(Project project) {
            DataContext = this;
            backlogView = new BacklogView();
            boardView = new BoardView();
            feedView = new ProjectFeedView();
            dashboardView = new ProjectDashboardView();

            CurrentProjectView = dashboardView;
            InitializeComponent();
            _project = project;
            GetProjectFeatures();
        }

        private void GetProjectFeatures() {
            try {
                Features = new ObservableCollection<Feature>(_featureManager.GetFeaturesByProjectID(_project.ProjectID));
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void btnDashboard_Click(object sender, System.Windows.RoutedEventArgs e) {
            CurrentProjectView = dashboardView;
        }

        private void btnFeed_Click(object sender, System.Windows.RoutedEventArgs e) {
            CurrentProjectView = feedView;
        }

        private void btnBacklog_Click(object sender, System.Windows.RoutedEventArgs e) {
            CurrentProjectView = backlogView;
        }

        private void btnBoard_Click(object sender, System.Windows.RoutedEventArgs e) {
            CurrentProjectView = boardView;
        }

        private void btnIssues_Click(object sender, System.Windows.RoutedEventArgs e) {

        }
    }
}
