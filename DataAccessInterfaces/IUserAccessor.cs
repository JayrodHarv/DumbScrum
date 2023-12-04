using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces {
    public interface IUserAccessor {
        User SelectUserByUserID(int userID);
        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
        UserVM SelectUserVMByEmail(string email);
        List<UserVM> SelectMembersByProjectID(string projectID);
        int InsertUser(string email, string passwordHash, byte[] pfp);
        int UpdateUser(User newUser, User oldUser);
        int CheckIfEmailHasBeenUsedAlready(string email);
        int UpdatePasswordHash(string email, string newPasswordHash);
        int UpdateDisplayName(int userID, string newDisplayName);
    }
}
