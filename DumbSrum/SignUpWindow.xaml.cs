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
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window {
        public SignUpWindow() {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e) {

        }

        private void hypSignUp_Click(object sender, RoutedEventArgs e) {
            var signInWindow = new SignInWindow();
            this.Close();
            signInWindow.ShowDialog();
        }
    }
}
