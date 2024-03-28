using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataObjects {
    public class Sprint {
        public int SprintID { get; set; }
        [Required]
        public string FeatureID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
    }

    public class SprintVM : Sprint {
        public string FeatureName { get; set; }

    }
}
