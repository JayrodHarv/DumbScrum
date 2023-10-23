using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects {
    public class Project {
        public string ProjectID { get; set; }
        public string ProjectOwner { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
    }

    public class ProjectVM : Project { 
        public List<User> Members { get; set; }

    }
}
