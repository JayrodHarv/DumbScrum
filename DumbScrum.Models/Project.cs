using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DumbScrum.Models {
    [Table("Project", Schema = "AppDbContext")]
    public partial class Project {
        [Key]
        [Required]
        [DisplayName("Project Name")]
        public required string ProjectId { get; set; }
        public required int UserId { get; set; }
        [Required]
        [DisplayName("Start Date")]
        public required DateTime DateCreated { get; set; }
        public required string Status { get; set; }
        public required string Description { get; set; }
    }

    public class ProjectVM : Project {
        [DisplayName("Project Owner")]
        public required string ProjectOwner {  get; set; }
    }
}
