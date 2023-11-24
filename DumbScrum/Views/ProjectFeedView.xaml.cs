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
    /// Interaction logic for ProjectFeedView.xaml
    /// </summary>
    public partial class ProjectFeedView : UserControl {
        public ProjectFeedView() {
            InitializeComponent();
        }

        private void txtInputText_KeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.Enter) {
                // do something
            }
        }
    }
}
