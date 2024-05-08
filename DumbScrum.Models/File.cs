

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DumbScrum.Models {
    [Table("File", Schema = "AppDbContext")]
    public partial class File {
        [Key]
        public int FileId { get; set; }
        public required byte[] Data { get; set; }
        public required string Extension { get; set; }
        public required int TaskID { get; set; }
        public required string ProjectId { get; set; }
        public required string FileName { get; set; }
        public required string Type { get; set; }
        public required DateTime LastEdited { get; set; }
    }
}
