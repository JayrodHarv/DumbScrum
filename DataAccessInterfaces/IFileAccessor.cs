using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces {
    public interface IFileAccessor {
        List<File> SelectTaskFilesByType(int taskID, string type);
        List<File> SelectProjectTemplateFiles(string projectID);
        int InsertTaskFile(File file);
        int InsertTemplateFile(File file);
        int UpdateFile(File oldFile, File newFile);
        int DeleteFile(int fileID);
    }
}
