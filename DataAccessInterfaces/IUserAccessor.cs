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
        int InsertUser(User user);
        int UpdateUser(User newUser, User oldUser);
        int CheckIfEmailHasBeenUsedAlready(string email);
        int UpdatePasswordHash(string email, string newPasswordHash);
        int UpdateDisplayName(int userID, string newDisplayName);
        List<string> SelectAllRoles();
        int DeleteUserRole(int userID, string roleID);
        int InsertUserRole(int userID, string roleID);
    }
}
