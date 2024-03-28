using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects {
    public class Feature {
        public string FeatureID { get; set; }
        public string ProjectID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Priority { get; set; }
        public string Status { get; set; }
    }

    public class FeatureVM : Feature {
        public int StoryCount { get; set; }
    }
}
