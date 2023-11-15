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
    /// Interaction logic for AddStoryWindow.xaml
    /// </summary>
    public partial class AddStoryWindow : Window {
        int featureID;
        public AddStoryWindow(int featureID) {
            InitializeComponent();
            this.featureID = featureID;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }

        private void btnAddStory_Click(object sender, RoutedEventArgs e) {
            if (txtPerson.Text == "") {
                MessageBox.Show("A user story must have a user.");
                return;
            }
            if (txtAction.Text == "") {
                MessageBox.Show("You must enter an action.");
                return;
            }
            if (txtReason.Text == "") {
                MessageBox.Show("You must give a reason.");
                return;
            }

            UserStoryManager userStoryManager = new UserStoryManager();

            try {
                if (userStoryManager.AddFeatureUserStory(featureID, txtPerson.Text, txtAction.Text, txtReason.Text)) {
                    this.DialogResult = true;
                } else {
                    MessageBox.Show("Failed to add user story to feature.");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
