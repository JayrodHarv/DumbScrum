using DataAccessFakes;
using DataObjects;
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
using System.Windows.Shapes;

namespace DumbSrum {
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window {
        UserManager _userManager = null;
        UserVM loggedInUser = null;
        bool isSigningUp = false;
        public WelcomeWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            // fake code that uses datafakes
            // _userManager = new UserManager(new UserAccessorFake());

            // real code that uses database
            _userManager = new UserManager();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e) {
            string password = pwdPassword.Password;
            string email = txtEmail.Text;

            // put some error checks here
            UserVM userVM = _userManager.SignInUser(email, password);
            if (userVM != null) {
                MessageBox.Show("Welcome " + userVM.DisplayName);
            } else {
                MessageBox.Show("Authentication failed");
            }
        }

        private void hypSignIn_Click(object sender, RoutedEventArgs e) {
            //var signUpWindow = new SignUpWindow();
            //this.Close();
            //signUpWindow.ShowDialog();
            if (!isSigningUp) {
                lblMessage.Content = "Sign up for a Dumb Scrum account";
                lblForgotPassword.Visibility = Visibility.Hidden;
                btnSignIn.Content = "Sign Up";
                hyperlinkText.Text = "Already have an account? Sign In";
                isSigningUp = true;
            } else {
                lblMessage.Content = "Sign in to a Dumb Scrum account";
                lblForgotPassword.Visibility = Visibility.Visible;
                btnSignIn.Content = "Sign In";
                hyperlinkText.Text = "Don't have an account? Sign Up";
                isSigningUp = false;
            }
        }

        private void hypForgotPassword_Click(object sender, RoutedEventArgs e) {

        }

        
    }
}
