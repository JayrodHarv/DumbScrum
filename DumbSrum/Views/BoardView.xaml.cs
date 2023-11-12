using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace DumbSrum.Views {
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl, INotifyPropertyChanged {
        SprintManager _sprintManager = new SprintManager();

        public ObservableCollection<Sprint> Sprints { get; set; }
        private SprintVM _currentSprint;

        public SprintVM CurrentSprint {
            get { return _currentSprint; }
            set {
                _currentSprint = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentSprint"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public BoardView(string projectID) {
            DataContext = this;

            try {
                Sprints = new ObservableCollection<Sprint>(_sprintManager.GetSprintsByProjectID(projectID));
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            InitializeComponent();
            cbxSprint.SelectedIndex = 0;
        }

        private void cbxSprint_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            CurrentSprint = _sprintManager.GetSprintVMBySprintID(Sprints[cbxSprint.SelectedIndex].SprintID);
            
            txtFeature.Text = CurrentSprint.FeatureID.ToString();
        }
    }
}
