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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DumbScrum.Views {
    /// <summary>
    /// Interaction logic for ProjectFeedView.xaml
    /// </summary>
    public partial class ProjectFeedView : UserControl {
        SprintManager sprintManager = new SprintManager();
        FeedMessageManager feedMessageManager = new FeedMessageManager();
        List<SprintVM> sprints = new List<SprintVM>();
        string projectID;
        int userID;
        public ProjectFeedView(string projectID, int userID) {
            this.projectID = projectID;
            this.userID = userID;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            btnCreatePost.IsDefault = true;
            try {
                sprints = sprintManager.GetSprintVMsByProjectID(projectID);
                if(sprints.Count > 0) {
                    cbxSprintFeed.ItemsSource = sprints;
                    DateTime now = DateTime.Now;
                    foreach (SprintVM s in sprints) {
                        if (now > s.StartDate && now <= s.EndDate) {
                            cbxSprintFeed.SelectedItem = s;
                        }
                    }
                    if (cbxSprintFeed.SelectedItem != null) {
                        SprintVM sprint = cbxSprintFeed.SelectedItem as SprintVM;
                        icFeedPosts.ItemsSource = feedMessageManager.GetSprintFeedMessages(sprint.SprintID);
                    }
                } else {
                    cbxSprintFeed.Visibility = Visibility.Collapsed;
                    lblThing.Content = "There are currently no sprints. Create a sprint to view and post messages to a feed.";
                }
                
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreatePost_Click(object sender, RoutedEventArgs e) {
            if(cbxSprintFeed.SelectedItem == null) {
                MessageBox.Show("Can't post to a feed that doesn't exist. Please start a sprint to get a feed.");
                return;
            }
            if(tbInputText.Text == "") {
                MessageBox.Show("You must enter a message in the text box to create a post.");
                return;
            }
            try {
                SprintVM sprint = cbxSprintFeed.SelectedItem as SprintVM;
                FeedMessage message = new FeedMessage() {
                    SprintID = sprint.SprintID,
                    UserID = userID,
                    Text = tbInputText.Text,
                    SentAt = DateTime.Now
                };
                if (feedMessageManager.CreateFeedMessage(message)) {
                    icFeedPosts.ItemsSource = feedMessageManager.GetSprintFeedMessages(sprint.SprintID);
                    tbInputText.Text = "";
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbxSprintFeed_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cbxSprintFeed.SelectedItem != null) {
                SprintVM sprint = cbxSprintFeed.SelectedItem as SprintVM;
                icFeedPosts.ItemsSource = feedMessageManager.GetSprintFeedMessages(sprint.SprintID);
            }
        }
    }
}
