using DataObjects;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DumbSrum {
    /// <summary>
    /// Interaction logic for ProjectWindow.xaml
    /// </summary>
    public partial class ProjectWindow : Window {
        private Project _projectVM = null;
        public ProjectWindow(Project projectVM) {
            InitializeComponent();
            _projectVM = projectVM;
        }
    }
}
