using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces {
    public interface IFileAccessor {
        List<File> SelectTaskFilesByType(int taskID, string type);
        int InsertFile(File file);
        int UpdateFile(File file);
        int DeleteFile(int fileID);
    }
}
