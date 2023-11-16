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

namespace DumbSrum.ToolWindows {
    /// <summary>
    /// Interaction logic for PlanNewSprint.xaml
    /// </summary>
    public partial class PlanNewSprintWindow : Window {
        string projectID = string.Empty;
        FeatureManager featureManager = new FeatureManager();
        public PlanNewSprintWindow(string projectID) {
            this.projectID = projectID;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            List<FeatureVM> features =  featureManager.GetFeaturesByProjectID(projectID);
            cboFeature.ItemsSource = features;
        }

        private void btnCreateProject_Click(object sender, RoutedEventArgs e) {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {

        }
    }
}
