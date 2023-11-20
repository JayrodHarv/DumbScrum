using System;
using System.Collections.Generic;

namespace DataObjects {
    public class Sprint {
        public int SprintID { get; set; }
        public int FeatureID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
    }

    public class SprintVM : Sprint {
        public string FeatureName { get; set; }

    }
}
