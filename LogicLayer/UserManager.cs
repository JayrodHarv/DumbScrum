using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer {
    public class UserManager : IUserManager {
        private IUserAccessor _userAccessor = null;

        public UserManager() {
            _userAccessor = new UserAccessor();
        }

        public UserManager(IUserAccessor userAccessor) {
            _userAccessor = userAccessor;
        }

        public bool AuthenticateUser(string email, string password) {
            bool result = false;
            password = HashSha256(password);
            result = (1 == _userAccessor.AuthenticateUserWithEmailAndPasswordHash(email, password));
            return result;
        }

        public bool ChangePassword(string email, string newPassword) {
            bool result = false;

            newPassword = HashSha256(newPassword);

            try {
                result = (1 == _userAccessor.UpdatePasswordHash(email, newPassword));
            } catch (Exception ex) {

                throw new ArgumentException("User or password not found.", ex);
            }

            return result;
        }

        public UserVM GetUserVMByEmail(string email) {
            UserVM userVM = null;

            try {
                userVM = _userAccessor.SelectUserVMByEmail(email);
            } catch (Exception ex) {
                throw new ApplicationException("User not found", ex);
            }

            return userVM;
        }

        public string HashSha256(string password) {
            string hashValue = "";
            // hash functions run at the bits and bytes level
            // so create a byte array
            byte[] data;

            // create .NET hash provider object
            using (SHA256 sha256Hasher = SHA256.Create()) {
                // hash the source string
                data = sha256Hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            // create an output string builder object
            var s = new StringBuilder();
            // loop through the byte array and build a return string
            for (int i = 0; i < data.Length; i++) {
                // outputs the byte as two hex digits
                s.Append(data[i].ToString("x2"));
            }
            hashValue = s.ToString();
            return hashValue;
        }

        public UserVM SignInUser(string email, string password) {
            UserVM userVM = null;
            try {
                if (AuthenticateUser(email, password)) {
                    userVM = GetUserVMByEmail(email);
                } else {
                    throw new ApplicationException("Bad email or password");
                }

            } catch (Exception ex) {
                throw new ApplicationException("Authentication Failed", ex);
            }
            return userVM;
        }

        public bool SignUpUser(string email, string password) {
            bool result = false;

            // check to see if an account already belongs to the email

            try {

            } catch (Exception) {

                throw;
            }


            password = HashSha256(password);
            try {
                result = (1 == _userAccessor.InsertUser(email, password));
            } catch (Exception ex) {
                throw new ApplicationException("Sign Up Failed", ex);
            }
            return result;
        }
    }
}
