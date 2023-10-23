using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer {
    public interface IUserManager {
        UserVM SignInUser(string email, string password);
        string HashSha256(string password);
        UserVM GetUserByEmail(string email);
    }
}
