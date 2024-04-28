using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DumbScrumWebMVC.Models {
    public class UseCasesVM {
        public int TaskID { get; set; }
        public TaskVM Task { get; set; }
        public List<File> Files { get; set; }
    }
}