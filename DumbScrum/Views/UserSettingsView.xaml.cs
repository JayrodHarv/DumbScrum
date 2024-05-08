using DataObjects;
using DumbScrum.UserControls;
using LogicLayer;
using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DumbScrum.Views {
    /// <summary>
    /// Interaction logic for UserSettingsView.xaml
    /// </summary>
    public partial class UserSettingsView : UserControl {
        MainManager _manager;
        ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
        UserVM newUser;
        public UserSettingsView() {
            _manager = MainManager.GetMainManager();
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            newUser = _manager.LoggedInUser;
            ImageSource pfp = (ImageSource)imageSourceConverter.ConvertFrom(_manager.LoggedInUser.Pfp);
            imgPfp.ImageSource = pfp;

            tbDisplayName.Text = _manager.LoggedInUser.DisplayName;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e) {
            if(tbDisplayName.Text == "") {
                MessageBox.Show("You can't have a blank name.");
                return;
            }
            newUser.DisplayName = tbDisplayName.Text;
            UserManager userManager = new UserManager();
            try {
                if (userManager.EditUser(newUser, _manager.LoggedInUser)) {
                    MainWindow window = (MainWindow)Window.GetWindow(this);
                    _manager.LoggedInUser = newUser;
                    window.txtDisplayName.Text = _manager.LoggedInUser.DisplayName;
                    ImageSource pfp = (ImageSource)imageSourceConverter.ConvertFrom(_manager.LoggedInUser.Pfp);
                    window.imgPfp.ImageSource = pfp;
                    MessageBox.Show("Settings Changed!");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangePfp_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            bool? result = fileDialog.ShowDialog();
            if (result == true) {
                ImageSource image = (ImageSource)imageSourceConverter.ConvertFrom(fileDialog.FileName);
                imgPfp.ImageSource = image;
                newUser.Pfp = GetPhotoData(fileDialog.FileName);
            }
        }

        private byte[] GetPhotoData(string filePath) {
            using (System.IO.Stream stream = System.IO.File.OpenRead(filePath)) {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e) {
            try {
                var passwordWindow = new ChangePasswordWindow();
                var result = passwordWindow.ShowDialog();
                if (result == true) {
                    MessageBox.Show("Password changed.", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                } else {
                    MessageBox.Show("Password not changed", "Operation Aborted",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                    "Update failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e) {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            WelcomeWindow welcomeWindow = new WelcomeWindow();
            window.Close();
            welcomeWindow.ShowDialog();
        }
    }
}
