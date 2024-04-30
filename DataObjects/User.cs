using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataObjects {
    public class User {
        public int UserID { get; set; }
        [DisplayName("Display Name")]
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [DisplayName("Profile Picture")]
        public byte[] Pfp { get; set; }
        public string Bio { get; set; }
        public bool Active { get; set; }
    }

    public class UserVM : User {
        public List<Task> Tasks { get; set; }
        public int InProgressTasksCount { get; set; }
        public int InReviewTasksCount { get; set; }
        public int CompletedTasksCount { get; set; }
    }
}
