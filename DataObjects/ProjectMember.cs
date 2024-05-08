using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects {
    public class ProjectMember {
        public int UserID { get; set; }
        public string ProjectID { get; set; }
        public int ProjectRoleID { get; set; }
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
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
        public byte[] Pfp { get; set; }
        [DisplayName("Project Role")]
        public int ProjectRoleId { get; set; }
        public string RoleName { get; set; }
        public bool Active { get; set; }

        // WEB
        public string Base64Image {
            get {
                return ImageHelper.ToBase64(Pfp);
            }
        }
    }
}
