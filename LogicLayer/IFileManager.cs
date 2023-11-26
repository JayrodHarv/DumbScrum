using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public interface IFileManager {
        List<File> GetTaskFilesByType(int taskID, string type);
        bool AddFile(File file);
        bool EditFile(File file);
        bool RemoveFile(int fileID);
    }
}
