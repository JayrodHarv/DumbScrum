using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects {
    public class ProjectMember {
        public int UserID { get; set; }
        public string ProjectID { get; set; }
        public string ProjectRoleID { get; set; }
        public bool Active { get; set; }
    }

    public class ProjectMemberVM {
        public User User { get; set; }
        public ProjectRole ProjectRole { get; set; }
        public bool Active { get; set; }
    }

    public class ProjectMemberListVM {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public byte[] Pfp { get; set; }
        public string ProjectRoleID { get; set; }
        public bool Active { get; set; }
    }
}
