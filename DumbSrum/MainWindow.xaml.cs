using DataObjects;
using LogicLayer;
using System.Windows;
using System.Windows.Input;

namespace DumbSrum {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        UserManager _userManager = new UserManager();
        UserVM loggedInUser = null;
        public MainWindow(UserVM user) {
            InitializeComponent();
            loggedInUser = user;
        }

        public MainWindow() { // Temperary
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) { 

        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e) {

        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e) {

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e) {
            if(e.LeftButton == MouseButtonState.Pressed) {
                DragMove();
            }
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e) {
            if(Application.Current.MainWindow.WindowState != WindowState.Maximized) {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            } else {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
                
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
