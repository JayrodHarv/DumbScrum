using DataObjects;
using DumbSrum.Views;
using LogicLayer;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace DumbSrum {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged {
        ProjectManager _projectManager = new ProjectManager();

        public UserVM LoggedInUser { get; set; }
        

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
            LoggedInUser = user;
            CurrentView = new HomeView();
            InitializeComponent();
            

            GetAllProjects();

            AppData.DataPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + "data";
        }

        private void GetAllProjects() {
            try {
                _projects = new ObservableCollection<Project>(_projectManager.GetAllProjects());
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            txtDisplayName.Text = LoggedInUser.DisplayName;
        }

        private void mnuHome_Click(object sender, RoutedEventArgs e) {
            CurrentView = new HomeView();
        }

        private void mnuMyProjects_Click(object sender, RoutedEventArgs e) {
            CurrentView = new MyProjectsView(LoggedInUser);
        }

        private void mnuBrowseProjects_Click(object sender, RoutedEventArgs e) {
            CurrentView = new ProjectListView();
        }
    }
}
