using DataObjects;
using DumbScrum.Views;
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

namespace DumbScrum.UserControls {
    /// <summary>
    /// Interaction logic for SrumBoardItem.xaml
    /// </summary>
    public partial class SrumBoardItem : UserControl {
        TaskManager taskManager = new TaskManager();
        UserManager userManager = new UserManager();
        TaskVM task;
        UserVM user;
        string projectID;
        public SrumBoardItem(TaskVM task, UserVM user, string projectID) {
            DataContext = task;
            this.task = task;
            this.user = user;
            this.projectID = projectID;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            if(task.Status == "To Do") {
                btnStart.Visibility = Visibility.Visible;
            } else {
                this.ToolTip = "Double-click to see more details";
            }

            tbTaskID.Text = task.TaskID.ToString();
            tbStory.Text = task.Story;
            if(task.UserID != 0) {
                spUserContainer.Visibility = Visibility.Visible;
                User taskUser = userManager.GetUser(task.UserID);
                txtUserDisplayName.Text = taskUser.DisplayName;
                ImageSourceConverter converter = new ImageSourceConverter();
                imgPfp.ImageSource = (ImageSource)converter.ConvertFrom(taskUser.Pfp);
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e) {
            try {
                if (taskManager.UpdateTaskUserID(task.TaskID, user.UserID)) {
                    MainWindow window = (MainWindow)Window.GetWindow(this);
                    ProjectView projectView = (ProjectView)window.CurrentView;
                    BoardView boardView = (BoardView)projectView.CurrentProjectView;
                    boardView.RefreshBoard(task.SprintID);
                    MessageBox.Show("Successfully claimed task");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (task.Status != "To Do") {
                MainWindow window = (MainWindow)Window.GetWindow(this);
                if(window.CurrentView.GetType() == typeof(HomeView)) {
                    window.CurrentView = new ProjectView(projectID, user);
                    ProjectView projectView = (ProjectView)window.CurrentView;
                    projectView.CurrentProjectView = new TaskView(projectID, task.TaskID, user);
                } else {
                    ProjectView projectView = (ProjectView)window.CurrentView;
                    projectView.CurrentProjectView = new TaskView(projectID, task.TaskID, user);
                }
            }
        }
    }
}
