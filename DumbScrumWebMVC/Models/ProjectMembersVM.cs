﻿using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DumbScrumWebMVC.Models {
    public class ProjectMembersVM {
        public string ProjectID { get; set; }
        public List<ProjectMemberListVM> Members { get; set; }
    }
}