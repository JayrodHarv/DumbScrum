using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DumbScrum.Models {
    [Table("Feature", Schema = "AppDbContext")]
    public partial class Feature {
        [Key]
        public required string FeatureId { get; set; }
        public required string ProjectId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Priority { get; set; }
        public required string Status { get; set; }
    }

    public class FeatureVM : Feature {
        public int StoryCount { get; set; }
    }
}
