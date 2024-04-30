using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects {
    public class Task {
        public int TaskID { get; set; }
        public int UserID { get; set; }
        public int SprintID { get; set; }
        public string StoryID { get; set; }
        public string Status { get; set; }
    }

    public class TaskVM : Task {
        public string ProjectName { get; set; }
        public string FeatureName { get; set; }
        public string UserDisplayName { get; set; }
        public string Story { get; set; }
    }
}
