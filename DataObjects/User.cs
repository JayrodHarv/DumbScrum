using System.Collections.Generic;

namespace DataObjects {
    public class User {
        public int UserID { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public byte[] Pfp { get; set; }
        public string Bio { get; set; }
        public bool Active { get; set; }
    }

    public class UserVM : User {
        public List<Task> Tasks { get; set; }
    }

    public class ProjectMemberVM : User {
        public List<Task> Tasks { get; set;}
    }
}
