using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer {
    public interface IUserManager {
        int GetUserIDFromEmail(string email);
        bool FindUser(string email);
        User GetUser(int userID);
        bool AuthenticateUser(string email, string password);
        UserVM SignInUser(string email, string password);
        UserVM SignUpUser(User user);
        string HashSha256(string password);
        UserVM GetUserVMByEmail(string email);
        bool EditUser(User newUser, User oldUser);
        bool ChangePassword(string email, string newPassword);
        bool ChangeDisplayName(int userID, string newDisplayName);
        List<string> GetAllRoles();
        bool RemoveUserRole(int userID, string roleID);
        bool AddUserRole(int userID, string roleID);
    }
}
