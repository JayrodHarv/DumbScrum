using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects {
    public static class ValidationHelpers {
        public static bool IsValidEmail(this string email) {
            bool isValid = false;

            if (email.Length > 13 && email.Length <= 100) {
                isValid = true;
            }

            return isValid;
        }

        public static bool IsValidPassword(this string password) {
            bool isValid = false;

            // this needs to be done right eventually
            if (password.Length >= 7) {
                isValid = true;
            }

            return isValid;
        }

    }
}
