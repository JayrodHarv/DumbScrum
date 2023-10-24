using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer {
    public interface IUserManager {
        bool AuthenticateUser(string email, string password);
        UserVM SignInUser(string email, string password);
        UserVM SignUpUser(string email, string password);
        string HashSha256(string password);
        UserVM GetUserVMByEmail(string email);
        bool ChangePassword(string email, string newPassword);
        bool ChangeDisplayName(int userID, string newDisplayName);
    }
}
