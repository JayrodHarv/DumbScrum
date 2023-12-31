﻿using System;
using System.Collections.Generic;

namespace DataObjects {
    public class Project {
        public string ProjectID { get; set; }
        public int UserID { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }

    public class ProjectVM : Project {
        public string ProjectOwner {  get; set; }
    }
}
