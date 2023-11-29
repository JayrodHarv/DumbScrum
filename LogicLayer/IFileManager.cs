using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public interface IFileManager {
        List<File> GetTaskFilesByType(int taskID, string type);
        List<File> GetProjectTemplateFiles(string projectID);
        bool AddTaskFile(File file);
        bool AddTemplateFile(File file);
        bool EditFile(File oldFile, File newFile);
        bool RemoveFile(int fileID);
    }
}
