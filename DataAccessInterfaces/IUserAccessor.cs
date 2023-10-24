using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces {
    public interface IUserAccessor {
        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
        UserVM SelectUserVMByEmail(string email);
        List<UserVM> SelectMembersByProjectID(string projectID);
        List<Project> SelectProjectsByUserID(int userID); // might be in seperate accessor
        int InsertUser(string email, string passwordHash);
        int UpdatePasswordHash(string email, string newPasswordHash);
        int UpdateDisplayName(int userID, string newDisplayName);
    }
}
