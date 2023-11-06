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

        public ProjectView(Project project) {
            backlogView = new BacklogView();

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
    }
}
