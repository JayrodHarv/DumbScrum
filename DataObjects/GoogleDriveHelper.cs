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
    public static class GoogleDriveHelper {
        private static string credentialsPath = "credentials.json";
        private static readonly string folderID = "1jV0OT_6UkKP-aHRS2h1xvAvGc2iQPYEs";
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
                    Parents = new List<string>() { folderID }
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

        public static bool UploadFileToProjectDriveFolder(string projectFolderID, string filePath) {
            try {
                // Upload file photo.jpg in specified folder on drive.
                var fileMetadata = new Google.Apis.Drive.v3.Data.File() {
                    Name = filePath,
                    Parents = new List<string>
                    {
                        projectFolderID
                    }
                };
                FilesResource.CreateMediaUpload request;
                // Create a new file on drive.
                using (var stream = new FileStream(filePath,
                           FileMode.Open)) {
                    // Create a new file, with metadata and stream.
                    request = service.Files.Create(
                        fileMetadata, stream, "image/jpeg");
                    request.Fields = "id";
                    request.Upload();
                }
                var file = request.ResponseBody;
                // Prints the uploaded file id.
                Console.WriteLine("File ID: " + file.Id);
                return file;
            } catch (Exception e) {
                // TODO(developer) - handle error appropriately
                if (e is AggregateException) {
                    Console.WriteLine("Credential Not found");
                } else if (e is FileNotFoundException) {
                    Console.WriteLine("File not found");
                } else if (e is DirectoryNotFoundException) {
                    Console.WriteLine("Directory Not found");
                } else {
                    throw;
                }
            }
            return null;
        }
    }
}
