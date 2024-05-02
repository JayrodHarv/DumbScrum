using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DumbScrumWebMVC.Models {
    public class TaskFilesVM {
        public string ProjectID { get; set; }
        public int TaskID { get; set; }
        public string FileType { get; set; }
        public HttpPostedFileBase UploadedFile { get; set; }
        public int SelectedFileID { get; set; }
        public List<File> Files { get; set; }
    }
}