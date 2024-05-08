using DataObjects;
using DumbScrum.Views;
using LogicLayer;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DumbScrum {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged {
        MainManager _manager;

        private object _currentView;

        public object CurrentView {
            get { return _currentView; }
            set {
                _currentView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentView"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow() {
            _manager = MainManager.GetMainManager();
            DataContext = this;
            CurrentView = new HomeView();
            InitializeComponent();

            // AppData.DataPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + "data";
            // GoogleDriveHelper.Init();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            txtDisplayName.Text = _manager.LoggedInUser.DisplayName;
            ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
            ImageSource pfp = (ImageSource)imageSourceConverter.ConvertFrom(_manager.LoggedInUser.Pfp);
            imgPfp.ImageSource = pfp;
            ImageSource logo = (ImageSource)imageSourceConverter.ConvertFrom(@".\Images\dumb_scrum_icon.png");
            imgLogo.Source = logo;
        }

        private void mnuHome_Click(object sender, RoutedEventArgs e) {
            CurrentView = new HomeView();
        }

        private void mnuMyProjects_Click(object sender, RoutedEventArgs e) {
            CurrentView = new MyProjectsView();
        }

        private void mnuBrowseProjects_Click(object sender, RoutedEventArgs e) {
            CurrentView = new ProjectListView();
        }

        private void userBox_Click(object sender, RoutedEventArgs e) {
            CurrentView = new UserSettingsView();
        }
    }
}
