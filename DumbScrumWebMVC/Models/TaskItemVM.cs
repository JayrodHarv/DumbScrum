﻿using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DumbScrumWebMVC.Models {
    public class TaskItemVM {
        public string ProjectID { get; set; }
        public TaskVM Task { get; set; }
    }
}