using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumbScrum.Models {
    public class UserStory {
        public string StoryId { get; set; }
        public string FeatureId { get; set; }
        public string Person { get; set; }
        public string Action { get; set; }
        public string Reason { get; set; }
    }

    public class UserStoryVM : UserStory {
        public string FeatureName { get; set; }
    }
}
