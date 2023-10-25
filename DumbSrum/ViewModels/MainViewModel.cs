using DumbSrum.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumbSrum.ViewModels {
    public class MainViewModel : ObservableObject {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ProjectsViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
		public ProjectsViewModel ProjectsVM { get; set; }

        private object _currentView;

		public object CurrentView {
			get { return _currentView; }
			set { 
				_currentView = value;
				OnPropertyChanged();
			}
		}

		public MainViewModel() {
			HomeVM = new HomeViewModel();
			ProjectsVM = new ProjectsViewModel();

			CurrentView = HomeVM;

			HomeViewCommand = new RelayCommand(o => { 
				CurrentView = HomeVM;
			});

            ProjectsViewCommand = new RelayCommand(o => {
                CurrentView = ProjectsVM;
            });
        }
    }
}
