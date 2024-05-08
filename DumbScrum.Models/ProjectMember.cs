using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumbScrum.Models {
    [Table("ProjectMember", Schema = "AppDbContext")]
    public class ProjectMember {
        [Key]
        public required int UserID { get; set; }
        [Required]
        [ForeignKey("ProjectId")]
        public string ProjectId { get; set; }
        public int ProjectRoleId { get; set; }
        public bool Active { get; set; }
    }

    public class ProjectMemberVM {
        public User User { get; set; }
        public ProjectRole ProjectRole { get; set; }
        public bool Active { get; set; }
    }

    public class ProjectMemberListVM {
        public int UserId { get; set; }
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
