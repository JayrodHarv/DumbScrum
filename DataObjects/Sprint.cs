﻿using System;
using System.Collections.Generic;

namespace DataObjects {
    public class Sprint {
        public int SprintID { get; set; }
        public string FeatureID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
    }

    public class SprintVM : Sprint {
        public string FeatureName { get; set; }

    }
}
