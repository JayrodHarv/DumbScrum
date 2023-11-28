using DataObjects;
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
        UserVM user;
        public UserSettingsView(UserVM user) {
            this.user = user;
            InitializeComponent();
        }


    }
}
