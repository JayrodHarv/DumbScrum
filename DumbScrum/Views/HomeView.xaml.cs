using DataObjects;
using DumbScrum.UserControls;
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
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl {
        TaskManager taskManager = new TaskManager();
        UserVM user;
        public HomeView(UserVM user) {
            this.user = user;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            lblWelcome.Content = "Welcome " + user.DisplayName + "!";
            try {
                List<TaskVM> tasks = taskManager.GetTaskVMsByUserID(user.UserID);
                icUserTasks.Items.Clear();
                foreach (TaskVM task in tasks) {
                    if(task.Status == "In Progress") {
                        icUserTasks.Items.Add(new SrumBoardItem(task, user, task.ProjectName));
                    }
                }
                tbInProgress.Text = tasks.FindAll(t => t.Status == "In Progress").Count().ToString();
                tbNeedsReviewed.Text = tasks.FindAll(t => t.Status == "Needs Reviewed").Count().ToString();
                tbComplete.Text = tasks.FindAll(t => t.Status == "Complete").Count().ToString();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
