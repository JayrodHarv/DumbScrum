using DataObjects;
using LogicLayer;
using System;
using System.Windows;

namespace DumbScrum {
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window {
        MainManager _manager;
        bool isSigningUp = false;
        public WelcomeWindow() {
            _manager = MainManager.GetMainManager();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            // fake code that uses datafakes
            // _userManager = new UserManager(new UserAccessorFake());

            // real code that uses database
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
                    _manager.LoggedInUser = _manager.UserManager.SignInUser(email, password);
                    var mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.ShowDialog();
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "Sign In Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            } else {
                // sign up user
                try {
                    string path = @"./Images/Sample_User_Icon.png";
                    byte[] pfp = _manager.UserManager.GetFileInBinary(path);
                    User user = new User() {
                        Email = email,
                        Password = password,
                        Pfp = pfp,
                        DisplayName = "New User"
                    };
                    _manager.LoggedInUser = _manager.UserManager.SignUpUser(user);
                    var mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.ShowDialog();
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "Sign Up Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void hypSignIn_Click(object sender, RoutedEventArgs e) {
            if (!isSigningUp) {
                txtEmail.Text = "";
                pwdPassword.Password = "";
                lblMessage.Content = "Sign up for a Dumb Scrum account";
                lblForgotPassword.Visibility = Visibility.Collapsed;
                spDisplayName.Visibility = Visibility.Visible;
                btnSignIn.Content = "Sign Up";
                hyperlinkText.Text = "Already have an account? Sign In";
                isSigningUp = true;
            } else {
                txtEmail.Text = "";
                pwdPassword.Password = "";
                lblMessage.Content = "Sign in to a Dumb Scrum account";
                lblForgotPassword.Visibility = Visibility.Visible;
                spDisplayName.Visibility = Visibility.Collapsed;
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
