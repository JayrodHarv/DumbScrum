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
        public UserAccessorFake() {
            fakeUsers.Add(new UserVM() {
                UserID = 1,
                DisplayName = "Joe Bidome",
                Email = "joe-bidome@gmail.com",
                Bio = "I am a dome."
            });
            fakeUsers.Add(new UserVM() {
                UserID = 2,
                DisplayName = "Obama Prism",
                Email = "obama-prism@gmail.com",
                Bio = "I am a prism."
            });
            fakeUsers.Add(new UserVM() {
                UserID = 3,
                DisplayName = "Donald Hump",
                Email = "donald-hump@gmail.com",
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

        public int CheckIfEmailHasBeenUsedAlready(string email) {
            throw new NotImplementedException();
        }

        public bool InsertUser(string email, string passwordHash) {
            throw new NotImplementedException();
        }

        public int InsertUser(string email, string passwordHash, byte[] pfp) {
            throw new NotImplementedException();
        }

        public List<UserVM> SelectMembersByProjectID(string projectID) {
            throw new NotImplementedException();
        }

        public List<Project> SelectProjectsByUserID(int userID) {
            throw new NotImplementedException();
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

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash) {
            throw new NotImplementedException();
        }

        public int UpdatePasswordHash(string email, string newPasswordHash) {
            throw new NotImplementedException();
        }

        int IUserAccessor.InsertUser(string email, string passwordHash, byte[] pfp) {
            throw new NotImplementedException();
        }
    }
}
