using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DumbScrum.Models {
    public class Sprint {
        public int SprintId { get; set; }
        [Required]
        public string FeatureId { get; set; }
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
