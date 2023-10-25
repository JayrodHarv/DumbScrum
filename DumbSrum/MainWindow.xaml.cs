using DataObjects;
using LogicLayer;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DumbSrum {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        UserManager _userManager = new UserManager();
        UserVM loggedInUser = null;
        string tab = "Home";
        public MainWindow(UserVM user) {
            InitializeComponent();
            loggedInUser = user;
            AppData.DataPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + "data";
        }

        public MainWindow() { // Temperary
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            txtDisplayName.Text = loggedInUser.DisplayName;
            lblTab.Content = tab;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e) {

        }

        private void btnProjects_Click(object sender, RoutedEventArgs e) {

        }

        private void btnChats_Click(object sender, RoutedEventArgs e) {

        }
    }
}
