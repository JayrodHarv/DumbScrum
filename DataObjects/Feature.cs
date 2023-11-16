using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects {
    public class Feature {
        public int FeatureID { get; set; }
        public string ProjectID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }

    public class FeatureVM : Feature {
        public int StoryCount { get; set; }
    }
}
