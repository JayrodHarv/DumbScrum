using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer {
    public class UserAccessor : IUserAccessor {
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash) {
            throw new NotImplementedException();
        }

        public bool InsertUser(string email, string passwordHash) {
            throw new NotImplementedException();
        }

        public List<UserVM> SelectMembersByProjectID(string projectID) {
            throw new NotImplementedException();
        }

        public List<Project> SelectProjectsByUserID(int userID) {
            throw new NotImplementedException();
        }

        public UserVM SelectUserVMByEmail(string email) {
            throw new NotImplementedException();
        }

        public int UpdateDisplayName(int userID, string newDisplayName) {
            throw new NotImplementedException();
        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash) {
            throw new NotImplementedException();
        }
    }
}
