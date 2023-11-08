using DataObjects;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;

namespace DumbSrum.Views {
    /// <summary>
    /// Interaction logic for MyProjectsView.xaml
    /// </summary>
    public partial class MyProjectsView : UserControl {
        public MyProjectsView() {
            InitializeComponent();
        }

        private void btnOpenProject_Click(object sender, RoutedEventArgs e) {
            MainWindow parentWindow = (MainWindow) Window.GetWindow(this);

            Project selectedProject = (Project)lvProjects.SelectedItem;

            parentWindow.CurrentView = new ProjectView(selectedProject.ProjectID);
        }
    }
}
