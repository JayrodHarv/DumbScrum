using DataObjects;
using LogicLayer;
using System;
using System.Windows;

namespace DumbSrum {
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window {
        public ChangePasswordWindow() {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e) {
            UserManager userManager = new UserManager();

            if(ValidationHelpers.IsValidPassword(pwdNewPassword.Password)) {
                if (pwdNewPassword.Password == pwdRetypePassword.Password) {
                    try {
                        if (userManager.ChangePassword(txtEmail.Text, pwdNewPassword.Password)) {
                            this.DialogResult = true;
                        }
                    } catch (Exception ex) {
                        MessageBox.Show(ex.Message);
                        this.DialogResult = false;
                    }
                } else {
                    MessageBox.Show("Retyped password must be the same as in above box.");
                }
            } else {
                MessageBox.Show("Password length must be greater than 7 characters");
            }
            
        }
    }
}
