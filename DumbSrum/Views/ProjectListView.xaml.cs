using DataObjects;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;

namespace DumbSrum.Views {
    /// <summary>
    /// Interaction logic for ProjectsView.xaml
    /// </summary>
    public partial class ProjectListView : UserControl {
        ProjectManager projectManager = new ProjectManager();
        public ProjectListView() {
            InitializeComponent();
        }

        private void btnOpenProject_Click(object sender, RoutedEventArgs e) {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            lvProjects.ItemsSource = projectManager.GetAllProjects();
        }
    }
}
