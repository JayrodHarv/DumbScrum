using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects {

    // Got to work but realized it'd be better and easier to just store the files in the database
    public static class GoogleDriveHelper {
        private static string credentialsPath = "credentials.json";
        private static readonly string projectFolderID = "1CWF1MjBe3bXN9I0Z0_3o6FeHPa9AKHi0";
        private static GoogleCredential credential;
        private static DriveService service;

        public static void Init() {
            try {
                using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read)) {
                    credential = GoogleCredential.FromStream(stream).CreateScoped(DriveService.Scope.DriveFile);
                }

                service = new DriveService(new BaseClientService.Initializer {
                    HttpClientInitializer = credential,
                    ApplicationName = "Dumb Scrum App"
                });
            } catch (Exception ex) {
                throw ex;
            }
            
        }

        public static string CreateProjectDriveFolder(string projectID) {
            try {
                // File metadata
                var fileMetadata = new Google.Apis.Drive.v3.Data.File() {
                    Name = projectID,
                    MimeType = "application/vnd.google-apps.folder",
                    Parents = new List<string>() { projectFolderID }
                };

                // Create a new folder on drive.
                var request = service.Files.Create(fileMetadata);
                request.Fields = "id";
                var file = request.Execute();
                return file.Id;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public static string CreateTaskDriveFolder(string projectFolderID, string taskName) {
            try {
                // File metadata
                var fileMetadata = new Google.Apis.Drive.v3.Data.File() {
                    Name = taskName,
                    MimeType = "application/vnd.google-apps.folder",
                    Parents = new List<string>() { projectFolderID }
                };

                // Create a new folder on drive.
                var request = service.Files.Create(fileMetadata);
                request.Fields = "id";
                var file = request.Execute();
                return file.Id;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public static string UploadFileToProjectDriveFolder(string folderID, string filePath) {
            try {
                var fileMetadata = new Google.Apis.Drive.v3.Data.File() {
                    Name = filePath,
                    Parents = new List<string>
                    {
                        folderID
                    }
                };
                FilesResource.CreateMediaUpload request;
                // Create a new file on drive.
                using (var stream = new FileStream(filePath,
                           FileMode.Open)) {
                    // Create a new file, with metadata and stream.
                    request = service.Files.Create(
                        fileMetadata, stream, "");
                    request.Fields = "id";
                    request.Upload();
                }
                var file = request.ResponseBody;
                return file.Id;
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
