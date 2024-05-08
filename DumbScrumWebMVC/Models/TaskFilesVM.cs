using DataObjects;
using System.Collections.Generic;

namespace DumbScrumWebMVC.Models {
    public class TaskFilesVM {
        public string ProjectID { get; set; }
        public TaskVM Task { get; set; }
        public string StoryID { get; set; }
        public string FileType { get; set; }
        public List<File> Files { get; set; }
    }
}