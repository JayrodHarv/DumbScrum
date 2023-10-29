using DataObjects;
using DumbSrum.Views;
using LogicLayer;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace DumbSrum {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged {

        // Views
        public HomeView homeView { get; set; }
        public ProjectListView projectListView { get; set; }
        public MyProjectsView myProjectsView { get; set; }

        public UserVM LoggedInUser { get; set; }

        UserManager _userManager = new UserManager();
        ProjectManager _projectManager = new ProjectManager();

        private object _currentView;

        public object CurrentView {
            get { return _currentView; }
            set {
                _currentView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentView"));
            }
        }


        private ObservableCollection<Project> _projects;

        public ObservableCollection<Project> Projects {
            get { return _projects; }
            set { _projects = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow(UserVM user) {
            DataContext = this;

            // View stuff
            homeView = new HomeView();
            projectListView = new ProjectListView();
            myProjectsView = new MyProjectsView();
            CurrentView = homeView; // this has to go before InitializeComponent

            InitializeComponent();
            LoggedInUser = user;
            AppData.DataPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + "data";
        }

        private void GetAllProjects() {
            try {
                _projects = new ObservableCollection<Project>(_projectManager.GetAllProjects());
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetUserProjects() {
            try {
                _projects = new ObservableCollection<Project>(_projectManager.GetProjectsByUserID(LoggedInUser.UserID));
            } catch (Exception) {

                throw;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            txtDisplayName.Text = LoggedInUser.DisplayName;
            lblTab.Content = "Home";
        }

        private void btnHome_Click(object sender, RoutedEventArgs e) {
            CurrentView = homeView;
            lblTab.Content = "Home";
        }

        private void btnBrowseProjects_Click(object sender, RoutedEventArgs e) {
            GetAllProjects();
            CurrentView = projectListView;
            lblTab.Content = "Browse All Projects";
        }

        private void btnMyProjects_Click(object sender, RoutedEventArgs e) {
            GetUserProjects();
            CurrentView = myProjectsView;
            lblTab.Content = "Projects";
        }
    }
}
