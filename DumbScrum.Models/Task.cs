using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumbScrum.Models {
    public class Task {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int SprintId { get; set; }
        public string StoryId { get; set; }
        public string Status { get; set; }
    }

    public class TaskVM : Task {
        public string ProjectName { get; set; }
        public string FeatureName { get; set; }
        public string UserDisplayName { get; set; }
        public string Story { get; set; }
    }
}
