using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes {
    public class UserAccessorFake : IUserAccessor {
        // Fake Users for testing purposes
        private List<UserVM> fakeUsers = new List<UserVM>();
        private List<string> passwordHashes = new List<string>();
        string path = @"./Images/Sample_User_Icon.png";
        public UserAccessorFake() {
            byte[] pfp = GetFileInBinary(path);
            fakeUsers.Add(new UserVM() {
                UserID = 100000,
                DisplayName = "Joe Bidome",
                Email = "joe-bidome@gmail.com",
                Pfp = pfp,
                Bio = "I am a dome."
            });
            fakeUsers.Add(new UserVM() {
                UserID = 100001,
                DisplayName = "Obama Prism",
                Email = "obama-prism@gmail.com",
                Pfp = pfp,
                Bio = "I am a prism."
            });
            fakeUsers.Add(new UserVM() {
                UserID = 100002,
                DisplayName = "Donald Hump",
                Email = "donald-hump@gmail.com",
                Pfp = pfp,
                Bio = "I am a hump."
            });
            passwordHashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e"); // newuser
            passwordHashes.Add("badhash");
            passwordHashes.Add("badhash");
        }

        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash) {
            int numAuthenticated = 0;

            // check for user records in the fake data
            for (int i = 0; i < fakeUsers.Count; i++) {
                if (passwordHashes[i] == passwordHash &&
                    fakeUsers[i].Email == email) {
                    numAuthenticated++;
                }
            }
            return numAuthenticated;
        }

        private byte[] GetFileInBinary(string filePath) {
            using (System.IO.Stream stream = System.IO.File.OpenRead(filePath)) {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public int CheckIfEmailHasBeenUsedAlready(string email) {
            throw new NotImplementedException();
        }

        public List<UserVM> SelectMembersByProjectID(string projectID) {
            throw new NotImplementedException();
        }

        public List<Project> SelectProjectsByUserID(int userID) {
            throw new NotImplementedException();
        }

        public User SelectUserByUserID(int userID) {
            User user = null;
            foreach (var fakeUser in fakeUsers) {
                if (fakeUser.UserID == userID) {
                    user = fakeUser;
                }
            }
            if (user == null) {
                throw new ApplicationException("User not found");
            }
            return user;
        }

        public UserVM SelectUserVMByEmail(string email) {
            UserVM userVM = null;
            foreach (var fakeUser in fakeUsers) {
                if (fakeUser.Email == email) {
                    userVM = fakeUser;
                }
            }
            if (userVM == null) {
                throw new ApplicationException("User not found");
            }
            return userVM;
        }

        public int UpdateDisplayName(int userID, string newDisplayName) {
            throw new NotImplementedException();
        }

        public int UpdatePasswordHash(string email, string newPasswordHash) {
            int rows = 0;
            for (int i = 0; i < fakeUsers.Count; i++) {
                if (fakeUsers[i].Email == email) {
                    passwordHashes[i] = newPasswordHash;
                    rows += 1;
                }
            }
            if (rows != 1) {
                throw new ApplicationException("Bad email or password");
            }
            return rows;
        }

        public int UpdateUser(User newUser, User oldUser) {
            throw new NotImplementedException();
        }

        int IUserAccessor.InsertUser(User user) {
            throw new NotImplementedException();
        }
    }
}
