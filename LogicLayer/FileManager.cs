using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public class FileManager : IFileManager {
        private IFileAccessor fileAccessor = null;

        public FileManager() {
            fileAccessor = new FileAccessor();
        }

        public FileManager(IFileAccessor fileAccessor) {
            this.fileAccessor = fileAccessor;
        }

        public bool AddTaskFile(File file) {
            bool result = false;
            try {
                result = (1 == fileAccessor.InsertTaskFile(file));
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public bool AddTemplateFile(File file) {
            bool result = false;
            try {
                result = (1 == fileAccessor.InsertTemplateFile(file));
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public bool EditFile(File oldFile, File newFile) {
            bool result = false;
            try {
                result = (1 == fileAccessor.UpdateFile(oldFile, newFile));
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public List<File> GetTaskFilesByType(int taskID, string type) {
            List<File> result = new List<File>();
            try {
                result = fileAccessor.SelectTaskFilesByType(taskID, type);
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public List<File> GetProjectTemplateFiles(string projectID) {
            List<File> result = new List<File>();
            try {
                result = fileAccessor.SelectProjectTemplateFiles(projectID);
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }

        public bool RemoveFile(int fileID) {
            bool result = false;
            try {
                result = (1 == fileAccessor.DeleteFile(fileID));
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }
    }
}
