using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DumbScrum.Models {
    [Table("FeedMessage", Schema = "AppDbContext")]
    public partial class FeedMessage {
        public required int MessageId { get; set; }
        public required int SprintId { get; set; }
        public required int UserId { get; set; }
        public required string Text { get; set; }
        [DisplayName("Sent At")]
        public required DateTime SentAt { get; set; }
    }

    public class FeedMessageVM : FeedMessage {
        [DisplayName("Sender")]
        public required string UserDisplayName { get; set; }
        [DisplayName("Sender Pfp")]
        public required byte[] UserPfp { get; set; }
    }
}
