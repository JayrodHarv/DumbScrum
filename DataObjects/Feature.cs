using System.ComponentModel.DataAnnotations;

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
