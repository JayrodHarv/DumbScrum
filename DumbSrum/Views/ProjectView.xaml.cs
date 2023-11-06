using DataObjects;
using System.ComponentModel;
using System.Windows.Controls;

namespace DumbSrum.Views {
    /// <summary>
    /// Interaction logic for ProjectView.xaml
    /// </summary>
    public partial class ProjectView : UserControl, INotifyPropertyChanged {
        public Project _project { get; set; }

        public BacklogView backlogView { get; set; }
        public BoardView boardView { get; set; }

        public ProjectView(Project project) {
            DataContext = this;
            backlogView = new BacklogView();
            boardView = new BoardView();

            CurrentProjectView = backlogView;
            InitializeComponent();
            _project = project;
        }

        private object _currentProjectView;

        public object CurrentProjectView {
            get { return _currentProjectView; }
            set { 
                _currentProjectView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentProjectView"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void btnDashboard_Click(object sender, System.Windows.RoutedEventArgs e) {

        }

        private void btnFeed_Click(object sender, System.Windows.RoutedEventArgs e) {

        }

        private void btnBacklog_Click(object sender, System.Windows.RoutedEventArgs e) {
            CurrentProjectView = backlogView;
        }

        private void btnBoard_Click(object sender, System.Windows.RoutedEventArgs e) {
            CurrentProjectView = boardView;
        }
    }
}
