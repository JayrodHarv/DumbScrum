using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer {
    public interface IUserManager {
        User GetUser(int userID);
        List<UserVM> GetProjectMembers(string projectID);
        bool AuthenticateUser(string email, string password);
        UserVM SignInUser(string email, string password);
        UserVM SignUpUser(string email, string password, byte[] pfp);
        string HashSha256(string password);
        UserVM GetUserVMByEmail(string email);
        bool EditUser(User newUser, User oldUser);
        bool ChangePassword(string email, string newPassword);
        bool ChangeDisplayName(int userID, string newDisplayName);
    }
}
