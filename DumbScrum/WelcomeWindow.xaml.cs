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
using static System.Net.Mime.MediaTypeNames;

namespace DumbScrum {
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
            btnSignIn.IsDefault = true;
            txtEmail.Focus();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e) {

            var email = txtEmail.Text;
            var password = pwdPassword.Password;

            // error checks
            if (!email.IsValidEmail()) {
                MessageBox.Show("That is not a valid email address", "Invalid Email Address",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                txtEmail.SelectAll();
                txtEmail.Focus();
                return;
            }
            if (!password.IsValidPassword()) {
                MessageBox.Show("That is not a valid password", "Invalid Password",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                pwdPassword.SelectAll();
                pwdPassword.Focus();
                return;
            }

            if (btnSignIn.Content.ToString() == "Sign In") {
                // try to sign in the user
                try {
                    loggedInUser = _userManager.SignInUser(email, password);
                    SignIn(loggedInUser);
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "Sign In Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            } else {
                // sign up user

                try {
                    string path = @"./Images/Sample_User_Icon.png";
                    byte[] pfp = GetFileInBinary(path);
                    loggedInUser = _userManager.SignUpUser(email, password, pfp);
                    SignIn(loggedInUser);
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "Sign Up Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private byte[] GetFileInBinary(string filePath) {
            using (System.IO.Stream stream = System.IO.File.OpenRead(filePath)) {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        private void SignIn(UserVM loggedInUser) {
            var mainWindow = new MainWindow(loggedInUser);
            this.Close();
            mainWindow.ShowDialog();
        }

        private void hypSignIn_Click(object sender, RoutedEventArgs e) {
            if (!isSigningUp) {
                txtEmail.Text = "";
                pwdPassword.Password = "";
                lblMessage.Content = "Sign up for a Dumb Scrum account";
                lblForgotPassword.Visibility = Visibility.Hidden;
                btnSignIn.Content = "Sign Up";
                hyperlinkText.Text = "Already have an account? Sign In";
                isSigningUp = true;
            } else {
                txtEmail.Text = "";
                pwdPassword.Password = "";
                lblMessage.Content = "Sign in to a Dumb Scrum account";
                lblForgotPassword.Visibility = Visibility.Visible;
                btnSignIn.Content = "Sign In";
                hyperlinkText.Text = "Don't have an account? Sign Up";
                isSigningUp = false;
            }
        }

        private void hypForgotPassword_Click(object sender, RoutedEventArgs e) {
            try {
                var passwordWindow = new ChangePasswordWindow();
                var result = passwordWindow.ShowDialog();
                if (result == true) {
                    MessageBox.Show("Password changed.", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                    "Update failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        } 
    }
}
