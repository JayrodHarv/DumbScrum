using DataObjects;
using DumbSrum.Views;
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

namespace DumbSrum.UserControls {
    /// <summary>
    /// Interaction logic for SrumBoardItem.xaml
    /// </summary>
    public partial class SrumBoardItem : UserControl {
        public SrumBoardItem() {
            InitializeComponent();
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            //ProjectView parent = this.Parent as ProjectView;
            //SrumBoardItem item = sender as SrumBoardItem;
            //int taskID = int.Parse(item.lblTaskID.Content.ToString());
            //parent.CurrentProjectView = new TaskView(taskID);
        }
    }
}
